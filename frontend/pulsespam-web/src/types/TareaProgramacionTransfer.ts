import { Pregunta } from "./Pregunta";

export interface TareaProgramacionTransfer {
    id: string,
    fechaIni: Date,
    fechaFin: Date,
    hora: string,
    preguntas: Pregunta[]
};