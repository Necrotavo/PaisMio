import { Input } from './input';

export class InputQ {
    cantidadDisponible: number;
    insumo: Input;

    constructor(cantidadDisponible: number, insumo: Input) {
        this.cantidadDisponible = cantidadDisponible;
        this.insumo = insumo;
    }
}
