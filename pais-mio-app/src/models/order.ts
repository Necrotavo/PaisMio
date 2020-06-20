import { ProductInOrder } from './productInOrder';
import { Client } from './client';
import { Analysis } from './analysis';
export class Order {
    codigo: number;
    cliente: Client;
    correoAdminIngreso: string;
    correoAdminDespacho: string;
    estado: string;
    fechaIngreso: string;
    fechaDespacho: string;
    listaProductos: ProductInOrder[];
    doAnalisisAA: Analysis;

    constructor(codigo: number, cliente: Client, correoAdminIngreso: string, listaProductos: ProductInOrder[]){

        this.codigo = codigo;
        this.cliente = cliente;
        this.correoAdminIngreso = correoAdminIngreso;
        this.listaProductos = listaProductos;

    }

}
