import { Categoria } from "./Categoria";

export interface PreguntaTransfer {
    id: string,
    categoriaId: string,
    tipoId: string,
    preguntaTxt: string,
    categoriaCat: string,
    tipo: string
};