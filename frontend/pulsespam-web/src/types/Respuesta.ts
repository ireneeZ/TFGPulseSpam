import { Categoria } from "./Categoria";

export interface Respuesta {
    id: string,
    tipo: string,
    respuestaLibre: string,
    respuestaSiNo: boolean,
    respuestaPuntuacion: number,
    fecha: Date
    usuarioId: string,
    preguntaId: string
};