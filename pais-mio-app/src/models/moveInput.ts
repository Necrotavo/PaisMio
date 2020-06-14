export class MoveInput {
    codigoDesdeBodega: number;
    codigoHastaBodega: number;
    codigoInsumo: number;
    cantidad: number;

    constructor(codigoDesdeBodega: number, codigoHastaBodega: number, codigoInsumo: number, cantidad: number){
        this.codigoDesdeBodega = codigoDesdeBodega;
        this.codigoHastaBodega = codigoHastaBodega;
        this.codigoInsumo = codigoInsumo;
        this.cantidad = cantidad;
    }
}
