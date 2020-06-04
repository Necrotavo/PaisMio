export class Input {
    codigo: number;
    nombre: string;
    cantMinStock: number;
    unidad: string;
    estado: string;

    constructor(codigo: number, nombre: string, cantMinStock: number, unidad: string, estado: string) {
        this.codigo = codigo;
        this.nombre = nombre;
        this.cantMinStock = cantMinStock;
        this.unidad = unidad;
        this.estado = estado;
    }
}
