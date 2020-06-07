export class Product {
    codigo: number;
    nombre: String;
    descripcion: String;
    estado: String;

    constructor (codigo : number, nombre: String, descripcion: String, estado: String){

        this.codigo = codigo;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.estado = estado;
    }
}
