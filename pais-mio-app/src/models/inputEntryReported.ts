import { EntryInput } from './entryInput';
export class InputEntryReported {
    codigo: number;
    correoAdministrador: string;
    fecha: string;
    listaInsumos: EntryInput[];

    constructor(codigo: number,
        correoAdministrador: string,
        fecha: string,
        listaInsumos: EntryInput[]){
            
        this.codigo = codigo;
        this.correoAdministrador = correoAdministrador;
        this.fecha = fecha;
        this.listaInsumos = listaInsumos;
    }
}