import { GridRowId } from "@mui/x-data-grid";
import { Pregunta } from "../types/Pregunta";

export const USUARIOS_GET = `https://localhost:5001/api/usuarios`;
export const USUARIOS_ID_DELETE = (id: GridRowId): string => `https://localhost:5001/api/usuarios/id/${id}`;
export const USUARIO_DELETE = (id: GridRowId): string => `https://localhost:5001/api/usuarios/${id}`;
export const CATEGORIA_GET = `https://localhost:5001/api/categorias`;
export const CATEGORIA_ID_GET = (id: string) => `https://localhost:5001/api/categorias/${id}`;
export const CATEGORIA_ID_DELETE = (id: GridRowId): string => `https://localhost:5001/api/categorias/${id}`;
export const DEPARTAMENTOS_GET = `https://localhost:5001/api/departamentos`;
export const PREGUNTA_GET = `https://localhost:5001/api/preguntas`;
export const PREGUNTA_ID_GET = (id: string) => `https://localhost:5001/api/preguntas/${id}`;
export const PREGUNTA_ID_DELETE = (id: GridRowId): string => `https://localhost:5001/api/preguntas/${id}`;
export const PROGRAMACION_GET = `https://localhost:5001/api/schedules`;
export const SCHED_FECHA_GET = (fecha: string) => `https://localhost:5001/api/schedules/${fecha}`;
export const TIPO_PREGUNTA_GET = `https://localhost:5001/api/tipos`;
export const TAREA_PROGRAMACION_GET = `https://localhost:5001/api/tareas`;
export const TAREA_PROGRAMACION_ID_GET = (id: string) => `https://localhost:5001/api/tareas/${id}`;
export const TAREA_PROGRAMACION_DELETE = (id: GridRowId): string => `https://localhost:5001/api/tareas/${id}`;

export const LOGIN = `https://localhost:5001/api/auth/login`;
export const REGISTRO = `https://localhost:5001/api/auth/registro`;

export const CATEGORIA_NINGUNA_ID = `6477b30b22a467cecb9144e4`;