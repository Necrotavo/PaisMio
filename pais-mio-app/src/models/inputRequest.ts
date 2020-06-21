import { InputQ } from './inputQ';

export class InputRequest {
    codigo: number;
    operario: string;
    codigoPedido: number;
    administrador: string;
    estado: string;
    fecha: string;
    bodega: number;
    insumosConsumo: InputQ[];
    insumosDescarte: InputQ[];
    notas: string;

    constructor(codigo: number, bodega: number, codigoPedido: number,
                insumosConsumo: InputQ[], insumosDescarte: InputQ[], operario: string,
                administrador: string, estado: string, fecha: string, notas: string) {
        this.codigo = codigo;
        this.bodega = bodega;
        this.codigoPedido = codigoPedido;
        this.insumosConsumo = insumosConsumo;
        this.insumosDescarte = insumosDescarte;
        this.operario = operario;
        this.estado = estado;
        this.administrador = administrador;
        this.fecha = fecha;
        this.notas = notas;
    }
}
