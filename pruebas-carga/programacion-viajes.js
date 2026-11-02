// Prueba de carga del apartado de programación de viajes.
//
// Reproduce lo que hace la pantalla al abrirse: pide la vista de viajes del
// mes en curso y los catálogos de los filtros. Cada usuario virtual repite
// ese ciclo con una pausa entre vueltas, como una persona que recarga y
// revisa.
//
// Las credenciales NO van en el archivo: se pasan por variable de entorno.
//
//   k6 run -e USUARIO=... -e CLAVE=... programacion-viajes.js
//
// Para apuntar a otro servidor:
//
//   k6 run -e BASE=http://localhost:5000/api -e USUARIO=... -e CLAVE=... programacion-viajes.js

import http from 'k6/http';
import { check, sleep } from 'k6';
import { Trend, Rate } from 'k6/metrics';

const BASE = __ENV.BASE || 'https://siil.hergarza.com.mx/api';
const ID_EMPRESA = __ENV.ID_EMPRESA || 2; // 1 Karve, 2 Vikingo, 3 Drakkar, 4 Letca

// Métricas propias: así el reporte separa las dos llamadas en vez de
// promediarlas juntas, que es donde se esconden los problemas.
const tiempoViajes = new Trend('viajes_ms', true);
const tiempoFiltros = new Trend('filtros_ms', true);
const fallos = new Rate('fallos');

export const options = {
  vus: 7,
  duration: '3m',

  // Si alguno de estos no se cumple, k6 termina con código de error. Son el
  // criterio de "pasa o no pasa", no adorno del reporte.
  thresholds: {
    'viajes_ms': ['p(95)<3000'],
    'filtros_ms': ['p(95)<1500'],
    'fallos': ['rate<0.01'],
    'http_req_failed': ['rate<0.01'],
  },

  // Prueba de techo: comenta `vus`/`duration` de arriba y descomenta esto
  // para ver a partir de cuántos usuarios se degrada. Correr fuera de horario.
  // stages: [
  //   { duration: '1m', target: 7 },
  //   { duration: '2m', target: 20 },
  //   { duration: '2m', target: 50 },
  //   { duration: '1m', target: 0 },
  // ],
};

/** Primer y último día del mes en curso, que es lo que pide la pantalla. */
function rangoDelMes() {
  const hoy = new Date();
  const dos = n => String(n).padStart(2, '0');

  const inicio = new Date(hoy.getFullYear(), hoy.getMonth(), 1);
  const fin = new Date(hoy.getFullYear(), hoy.getMonth() + 1, 0);

  const formatear = f => `${f.getFullYear()}-${dos(f.getMonth() + 1)}-${dos(f.getDate())}`;

  return { inicio: formatear(inicio), fin: formatear(fin) };
}

// setup() corre una sola vez, antes que los usuarios virtuales. El token se
// saca aquí para no medir siete logins que la pantalla real no hace.
export function setup() {
  const usuario = __ENV.USUARIO;
  const clave = __ENV.CLAVE;

  if (!usuario || !clave) {
    throw new Error('Faltan credenciales: pasa -e USUARIO=... -e CLAVE=...');
  }

  const respuesta = http.post(
    `${BASE}/UsuariosSistema`,
    JSON.stringify({ Username: usuario, Password: clave }),
    { headers: { 'Content-Type': 'application/json' } }
  );

  if (respuesta.status !== 200) {
    throw new Error(`El login devolvió ${respuesta.status}`);
  }

  const token = respuesta.json('token');

  if (!token) {
    throw new Error('El login no devolvió token');
  }

  return { token };
}

export default function (datos) {
  const { inicio, fin } = rangoDelMes();

  const parametros = {
    headers: { Authorization: `Bearer ${datos.token}` },
  };

  // 1. La tabla. Es la pesada: trae la vista completa del mes.
  const viajes = http.get(
    `${BASE}/ProgramacionViajes/filtrar-vista-completa` +
      `?fechaInicio=${inicio}&fechaFin=${fin}&idEmpresa=${ID_EMPRESA}`,
    { ...parametros, tags: { nombre: 'viajes' } }
  );

  tiempoViajes.add(viajes.timings.duration);

  const viajesBien = check(viajes, {
    'viajes responde 200': r => r.status === 200,
    'viajes trae datos': r => r.body.length > 2,
  });

  // 2. Los catálogos de la card de filtros, que la pantalla pide en paralelo.
  const filtros = http.get(
    `${BASE}/Empresas/${ID_EMPRESA}/filtros-busqueda`,
    { ...parametros, tags: { nombre: 'filtros' } }
  );

  tiempoFiltros.add(filtros.timings.duration);

  const filtrosBien = check(filtros, {
    'filtros responde 200': r => r.status === 200,
  });

  fallos.add(!(viajesBien && filtrosBien));

  // Pausa entre vueltas. Sin esto siete usuarios virtuales pegan miles de
  // veces por minuto y mides un martillazo, no a siete personas trabajando.
  sleep(Math.random() * 5 + 5);
}
