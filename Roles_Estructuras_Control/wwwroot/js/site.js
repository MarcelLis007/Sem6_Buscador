$().ready(

    () => {
        detallefactura()
    }

);

var detallefactura = () => {
    var leerClientes = new DetalleFactura()
    leerClientes.listaClientes()
}

var unCliente = () => {
    var id = $('#listaClientes').val()
    var uncliente = new DetalleFactura()
    uncliente.unCliente(id)
}

var nuevoCliente = () => {
    var nuevoCliente = new DetalleFactura()
    nuevoCliente.nuevoCliente()
}

var limpiarcajas = () => {
    var limpiarcajas = new DetalleFactura()
    limpiarcajas.limpiarCampos()
}

var listaProductos = () => {
    var listaProductos = new DetalleFactura()
    listaProductos.listaProductos()

}

var controlarstock = (caja) => {
    var controlarstock = new DetalleFactura()
    controlarstock.controlarstock(caja.id, caja.value)
}


var cargarProdcutosLista = (id, precio, nombre) => {
    var cargarProdcutosLista = new DetalleFactura()
    cargarProdcutosLista.cargarProdcutosLista(id, precio, nombre);
}