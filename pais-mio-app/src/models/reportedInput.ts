import { Input } from './input';
export class ReportedInput {
    cantidadConsumida: number;
    cantidadDescartada: number;
    insumo: Input;
    total: number;

    constructor(cantidadConsumida: number, cantidadDescartada: number, insumo: Input, total: number){
        this.cantidadConsumida = cantidadConsumida;
        this.cantidadDescartada = cantidadDescartada;
        this.insumo = insumo;
        this.total = total;
    }
}