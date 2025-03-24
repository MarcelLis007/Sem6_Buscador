class DetalleFactura {

    constructor() {
    }

    listaClientes() {
        var html = "<option value=0>Seleccione un opcion</option>"
        $.get("../../clientes/ListaClientes", (listaclientes) => {
            
            $.each(listaclientes, (index, valor) => {
                html += `<option value=${valor.id}>${valor.nombre}</option>` 
            })
            $("#listaClientes").html(html)
        })
    }

    unCliente(id) {
        $.get("../../clientes/unCliente?id="+id, (cliente) => {
            console.log(cliente)
            $("#NombreCliente").val(cliente.nombre)                        
            $("#DireccionCliente").val(cliente.direccion)                        
            $("#TelefonoCliente").val(cliente.telefono)                        
            $("#EmailCliente").val(cliente.email)                        
        })
    }
    nuevoCliente() {
        $("#NombreCliente").prop('disabled', false)
        $("#DireccionCliente").prop('disabled', false)
        $("#TelefonoCliente").prop('disabled', false)
        $("#EmailCliente").prop('disabled', false)
        $("#listaClientes").prop('disabled', true)
        $("#nuevoCliente").val(1)
        $("#cancelar").css('display', "block")
    }

    listaProductos() {
        var html = ""
        $.get("../../productos/ListaProductos", (listaprodcutos) => {
            
            $.each(listaprodcutos, (index, stock) => {
                console.log(stock)
                html += `<tr>
                <td>${(index + 1)} </td>
                <td>${stock["productoModels"]["nombreProducto"]} </td>
                <td>
                    <div class="input-group has-validation">
                        <input type="number" id="stock-${stock.id}" onfocusout="controlarstock(this)" class="form-control" style="width:60px">
                        <div id="validador${stock.id}" style="visibility:hidden">*
                        </div>
                    </div>
                </td>
                <td>${stock.precioVenta}</td>
                <td>
                    <button onclick="cargarProdcutosLista(${stock.id},${stock.precioVenta},'${stock["productoModels"]["nombreProducto"]}')" class="btn btn-outline-success">
                    <i class="icon-plus"></i>
                    </button>
                </td>
                </tr>
                `
            })
            $("#cuerpoproducto").html(html)
        })
    }

    controlarstock(id, cantidad) {
        if (cantidad <= 0 || !cantidad || cantidad == null || cantidad == null) return
        id = id.split("-")


        $.post("../stocks/controlarstock", {id:id[1],cantidad:cantidad}, (dato) => {
            if (!dato) {
                alert("No se encuentra la cantidad en stock")
                $("#btn_agregar").prop('disabled', true)
            
            } else {
                $("#btn_agregar").prop('disabled', false)
            }
        })
    }


    cargarProdcutosLista(id, precio, nombre) {
        
        var cantidad = $(`#stock-${id}`).val()
        if (!cantidad || cantidad <= 0) {
            alert("Ingrese una cantidad valida")
            return
        }

       var html = `<tr>
            <td>
                #
            </td>
            <td>
                ${cantidad}
            </td>
            <td>
                ${nombre}
            </td>
            <td>
                ${precio}
            </td>
            <td>
                ${cantidad * precio}
            </td>

        </tr>`
        $("#tablaclientes").append(html)

        $("#productos").modal("hide")

    }

    limpiarCampos() {
        $("#NombreCliente").prop('disabled', true)
        $("#DireccionCliente").prop('disabled', true)
        $("#TelefonoCliente").prop('disabled', true)
        $("#EmailCliente").prop('disabled', true)
        $("#listaClientes").prop('disabled', false)
        $("#nuevoCliente").val(0)
        $("#cancelar").css('display', "none")
    }
}