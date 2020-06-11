export class Product {
    codigo: number;
    id: string;
    nombre: string;
    descripcion: string;
    estado: string;

    constructor(codigo: number, id: string, nombre: string, descripcion: string, estado: string){

        this.codigo = codigo;
        this.id = id;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.estado = estado;
    }
}
