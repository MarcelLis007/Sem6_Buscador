$(document).ready(function () {
    // Variables globales para manejar los productos seleccionados
    let productosSeleccionados = [];
    let contadorFilas = 0;
    let productoSeleccionadoTemp = null; 

    $('#modalBuscarProductos').on('shown.bs.modal', function () {
        cargarProductosEnModal('');
        $('#txtBuscarProducto').val('').focus();
    });

   
    $('#txtBuscarProducto').on('input', function () {
        const filtro = $(this).val().trim();
        cargarProductosEnModal(filtro);
    });

   
    $('#btnBuscarProducto').on('click', function () {
        const filtro = $('#txtBuscarProducto').val().trim();
        cargarProductosEnModal(filtro);
    });

    
    $('#txtBuscarProducto').on('keypress', function (e) {
        if (e.which === 13) { 
            e.preventDefault();
        }
    });

    // Función para cargar productos en el modal 
    let timerId;
    function cargarProductosEnModal(filtro) {
        
        clearTimeout(timerId);

        timerId = setTimeout(function () {
            $.ajax({
                url: '../../Productos/BuscarProductos',
                type: 'GET',
                data: { filtro: filtro },
                beforeSend: function () {
                   
                    $('#tablaProductosModal tbody').html('<tr><td colspan="5" class="text-center"><div class="spinner-border spinner-border-sm" role="status"></div> Cargando...</td></tr>');
                },
                success: function (productos) {
                    let html = '';
                    if (productos.length === 0) {
                        html = '<tr><td colspan="5" class="text-center">No se encontraron productos</td></tr>';
                    } else {
                        $.each(productos, function (index, producto) {
                            html += `<tr>
                                <td>${index + 1}</td>
                                <td>${producto.nombre_Producto}</td>
                                <td>${producto.presentacion}</td>
                                <td>${producto.codigoBarras}</td>
                                <td>
                                    <button class="btn btn-sm btn-primary btn-seleccionar-producto"
                                            data-id="${producto.id}"
                                            data-nombre="${producto.nombre_Producto}"
                                            data-presentacion="${producto.presentacion}"
                                            data-codigo="${producto.codigoBarras}">
                                        Seleccionar
                                    </button>
                                </td>
                            </tr>`;
                        });
                    }
                    $('#tablaProductosModal tbody').html(html);

                   
                    $('.btn-seleccionar-producto').on('click', function () {
                        const id = $(this).data('id');
                        const nombre = $(this).data('nombre');
                        const presentacion = $(this).data('presentacion');
                        const codigo = $(this).data('codigo');

                        
                        $.get('../../DetalleFactura/ObtenerValorPorProducto', { productoId: id }, function (data) {
                            const valor = data.valor;

                           
                            productoSeleccionadoTemp = {
                                id: id,
                                nombre: nombre,
                                presentacion: presentacion,
                                codigo: codigo,
                                valor: valor // Aquí asignamos el valor obtenido
                            };

                            // Mostrar información del producto seleccionado en un área visible
                            mostrarProductoSeleccionado(productoSeleccionadoTemp);

                            // Cerrar el modal
                            $('#modalBuscarProductos').modal('hide');

                            // Habilitar el botón de agregar
                            $('#btnAgregarProducto').prop('disabled', false);
                        }).fail(function () {
                            alert('Error al obtener el valor del producto');
                        });
                    });
                },
                error: function (error) {
                    console.error('Error al buscar productos:', error);
                    $('#tablaProductosModal tbody').html('<tr><td colspan="5" class="text-center text-danger">Error al cargar productos</td></tr>');
                }
            });
        }, 300); 
    }

   
    function mostrarProductoSeleccionado(producto) {
        const html = `
            <div class="alert alert-info" id="productoSeleccionadoInfo">
                <strong>Producto seleccionado:</strong> ${producto.nombre} (${producto.presentacion})
                <div>Código: ${producto.codigo} | Valor: ${formatearPrecio(producto.valor)}</div>
                <div class="mt-2">
                    <label>Cantidad:</label>
                    <input type="number" id="cantidadProductoTemp" class="form-control form-control-sm d-inline-block" 
                           style="width: 100px;" value="1" min="1">
                </div>
            </div>
        `;

        // Mostrar el producto seleccionado en untabla
        $('#areaProductoSeleccionado').html(html);
    }

    
    $('#btnAgregarProducto').on('click', function () {
        if (!productoSeleccionadoTemp) {
            alert('Primero debe seleccionar un producto');
            return;
        }

        const cantidad = parseInt($('#cantidadProductoTemp').val() || 1);
        if (cantidad < 1) {
            alert('La cantidad debe ser mayor a cero');
            return;
        }

        
        agregarProductoATabla(
            productoSeleccionadoTemp.id,
            productoSeleccionadoTemp.nombre,
            productoSeleccionadoTemp.presentacion,
            productoSeleccionadoTemp.codigo,
            productoSeleccionadoTemp.valor,
            cantidad
        );

     
        productoSeleccionadoTemp = null;
        $('#areaProductoSeleccionado').html('');

     
        $(this).prop('disabled', true);
    });

    function agregarProductoATabla(id, nombre, presentacion, codigo, valor, cantidad = 1) {
        
        const productoExistente = productosSeleccionados.find(p => p.id === id);

        if (productoExistente) {
            
            const nuevaCantidad = productoExistente.cantidad + cantidad;

            
            productoExistente.cantidad = nuevaCantidad;
            productoExistente.total = nuevaCantidad * productoExistente.valor;

           
            $(`#fila-producto-${id} .cantidad-producto`).val(nuevaCantidad);
            $(`#fila-producto-${id} .precio-total`).text(formatearPrecio(productoExistente.total));
        } else {
            
            contadorFilas++;

           
            const producto = {
                id: id,
                nombre: nombre,
                presentacion: presentacion,
                codigo: codigo,
                valor: valor, 
                cantidad: cantidad,
                total: valor * cantidad
            };

          
            productosSeleccionados.push(producto);

            const html = `
                <tr id="fila-producto-${id}" data-id="${id}">
                    <td>${contadorFilas}</td>
                    <td>${nombre} (${presentacion})</td>
                    <td>
                        <input type="number" class="form-control form-control-sm cantidad-producto" 
                               value="${cantidad}" min="1" data-id="${id}">
                    </td>
                    <td class="precio-unitario">${formatearPrecio(valor)}</td>
                    <td class="precio-total">${formatearPrecio(valor * cantidad)}</td>
                    <td>
                        <button class="btn btn-sm btn-danger btn-eliminar-producto" data-id="${id}">
                            <i class="fas fa-trash"></i>
                        </button>
                    </td>
                </tr>
            `;

            // Agregar a la tabla principal
            $('#tablaProductosPrincipal tbody').append(html);

            
            $(`#fila-producto-${id} .cantidad-producto`).on('change', function () {
                const productoId = $(this).data('id');
                const cantidadNueva = parseInt($(this).val());

                if (cantidadNueva < 1) {
                    $(this).val(1);
                    return;
                }

                // Actualizar el objeto en el array
                const index = productosSeleccionados.findIndex(p => p.id === productoId);
                if (index !== -1) {
                    productosSeleccionados[index].cantidad = cantidadNueva;
                    productosSeleccionados[index].total = cantidadNueva * productosSeleccionados[index].valor;

                    
                    const total = productosSeleccionados[index].total;
                    $(`#fila-producto-${productoId} .precio-total`).text(formatearPrecio(total));
                }
            });

          
            $(`#fila-producto-${id} .btn-eliminar-producto`).on('click', function () {
                const productoId = $(this).data('id');

                const index = productosSeleccionados.findIndex(p => p.id === productoId);
                if (index !== -1) {
                    productosSeleccionados.splice(index, 1);
                }

               
                $(`#fila-producto-${productoId}`).remove();

                // Actualizar total
                actualizarTotalesTabla();
            });
        }

        
        actualizarTotalesTabla();
    }

  
    function actualizarTotalesTabla() {
        let totalGeneral = 0;

        productosSeleccionados.forEach(producto => {
            totalGeneral += producto.total;
        });

        
        $('#totalGeneral').text(formatearPrecio(totalGeneral));

       
        $('#contadorProductos').text(productosSeleccionados.length);
    }

    function formatearPrecio(valor) {
        return '$' + parseFloat(valor).toFixed(2);
    }

   
    $('#btnGuardarDatos').on('click', function () {
        if (productosSeleccionados.length === 0) {
            alert('Debe seleccionar al menos un producto');
            return;
        }

       
        const datosAGuardar = {
            cliente: {
               
                id: $('#clienteId').val(),
                nombre: $('#clienteNombre').val()
            
            },
            productos: productosSeleccionados,
            total: productosSeleccionados.reduce((sum, producto) => sum + producto.total, 0)
        };

        console.log('Datos a guardar:', datosAGuardar);

     
        $.ajax({
            url: '../../Ventas/GuardarVenta',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(datosAGuardar),
            success: function (response) {
                if (response.success) {
                    alert('Datos guardados correctamente');
                    
                    productosSeleccionados = [];
                    contadorFilas = 0;
                    $('#tablaProductosPrincipal tbody').empty();
                    $('#areaProductoSeleccionado').html('');
                    productoSeleccionadoTemp = null;
                    $('#btnAgregarProducto').prop('disabled', true);
                    actualizarTotalesTabla();
                } else {
                    alert('Error al guardar: ' + response.message);
                }
            },
            error: function (error) {
                console.error('Error al guardar datos:', error);
                alert('Ocurrió un error al guardar los datos');
            }
        });
    });
});