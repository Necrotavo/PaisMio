import { ProductInOrder } from './productInOrder';
import { Client } from './client';
export class Order {
    codigo: number;
    cliente: Client;
    correoAdminIngreso: string;
    correoAdminDespacho: string;
    estado: string;
    fecheIngreso: string;
    fechaDespacho: string;
    listaProductos: ProductInOrder[];

    constructor(codigo: number, cliente: Client, correoAdminIngreso: string, listaProductos: ProductInOrder[]){

        this.codigo = codigo;
        this.cliente = cliente;
        this.correoAdminIngreso = correoAdminIngreso;
        this.listaProductos = listaProductos;

    }

}
