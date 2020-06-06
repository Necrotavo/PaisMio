import { InputQ } from './inputQ';

export class InputRequest {
    bodega: number;
    codigoPedido: number;
    insumosConsumo: InputQ[];
    insumosDescarte: InputQ[];
    operario: string;
    codigo: number;
    admin: string;
    estado: string;

    constructor(bodega: number, codigoPedido: number, insumosConsumo: InputQ[], insumosDescarte: InputQ[], operario: string) {
        this.bodega = bodega;
        this.codigoPedido = codigoPedido;
        this.insumosConsumo = insumosConsumo;
        this.insumosDescarte = insumosDescarte;
        this.operario = operario;
    }
}
