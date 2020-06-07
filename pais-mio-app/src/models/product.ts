export class Product {
    codigo: number;
    nombre: string;
    descripcion: string;
    estado: string;

    constructor(codigo: number, nombre: string, descripcion: string, estado: string){

        this.codigo = codigo;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.estado = estado;
    }
}
