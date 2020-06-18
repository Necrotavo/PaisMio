import { InputQ } from './inputQ';
export class Cellar {
    codigo: number;
    estado: string;
    nombre: string;
    direccion: string;
    telefono: string;
    listaInsumosEnBodega: InputQ[];

    constructor(codigo: number, estado: string, nombre: string, direccion: string, telefono: string, listaInsumosEnBodega: InputQ[]){
        this.codigo = codigo;
        this.estado = estado;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.listaInsumosEnBodega = listaInsumosEnBodega;
    }
}
