export class Input {
    codigo: number;
    nombre: string;
    cantMinStock: number;
    unidad: string;
    estado: string;
    id: string;

    constructor(codigo: number, nombre: string, cantMinStock: number, unidad: string, estado: string, id: string) {
        this.codigo = codigo;
        this.nombre = nombre;
        this.cantMinStock = cantMinStock;
        this.unidad = unidad;
        this.estado = estado;
        this.id = id;
    }
}
