export class User {
    correo: string;
    estado: string;
    nombre: string;
    apellidos: string;
    contrasena: string;
    
    constructor(correo: string, estado: string, nombre: string, apellidos: string, contrasena: string) {
            this.correo = correo;
            this.estado = estado;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.contrasena = contrasena;
    }
}
