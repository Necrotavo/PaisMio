import { InputQ } from './inputQ';

export class InputRequest {
    codigo: number;
    operario: string;
    codigoPedido: number;
    admin: string;
    estado: string;
    fecha: Date;
    bodega: number;
    insumosConsumo: InputQ[];
    insumosDescarte: InputQ[];

    constructor(codigo: number, bodega: number, codigoPedido: number,
                insumosConsumo: InputQ[], insumosDescarte: InputQ[], operario: string,
                admin: string, estado: string, fecha: Date) {
             this.codigo = codigo;
             this.fecha = fecha;
             this.bodega = bodega;
             this.codigoPedido = codigoPedido;
             this.insumosConsumo = insumosConsumo;
             this.insumosDescarte = insumosDescarte;
             this.operario = operario;
             this.estado = estado;
             this.admin = admin;
             this.fecha = fecha;

    }

}
