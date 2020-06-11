export class User {
    correo: string;
    estado: string;
    nombre: string;
    apellidos: string;
    contrasena: string;
    rol: string;

    constructor(correo: string, estado: string, nombre: string, apellidos: string, contrasena: string, rol: string) {
            this.correo = correo;
            this.estado = estado;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.contrasena = contrasena;
            this.rol = rol;
    }

}
