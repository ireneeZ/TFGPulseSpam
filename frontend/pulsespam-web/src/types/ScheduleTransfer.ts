import { Pregunta } from "./Pregunta";
import { Respuesta } from "./Respuesta";

export interface ScheduleTransfer {
    id: string,
    pregunta: Pregunta,
    fechaPregunta: Date,
    respuestas: Respuesta[]
};