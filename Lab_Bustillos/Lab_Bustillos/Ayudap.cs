using System.Windows.Forms;

namespace Lab_Bustillos
{
    public partial class AyudaP : Form
    {
        public static int opcion = 0;
        public AyudaP()
        {
            InitializeComponent();
            Labe();
        }
        public void Labe()
        {
            if (opcion == 0)
            {
                label1.Text = "La ventana personal registra a los usuarios que van a entrar al sistema. " +
                "Editar: al seleccionar una fila y presionar este botón los parámetros de la fila se pasan a los campos de texto para ser modificados." +
                "Eliminar: al seleccionar una fila, esta se elimina. " +
                "Al llenar los campos de texto y dar click en guardar los datos se registran en la base de datos. " +
                "En el campo de búsqueda se hace un filtro de los datos para un mejor manejo de los datos." +
                "El contador depende de cada usuario y este lleva el conteo de los clics que el usuario da en esta ventana.";
            }
            else if (opcion == 1)
            {
                label1.Text = "En la ventana Restaurar se lleva a cabo la restauración de los datos eliminados, modificados y de los insertados." +
                             "Se selecciona una opción del combo y depende de la opción son los datos que se muestran" +
                             "Insertados: son todos los datos que han sido registrados. " +
                             "Eliminados: Son todos los datos que han sido eliminados. " +
                             "Modificados: esta tabla guarda los datos anteriores a ser modificados." +
                                "Restaurar: Restaura los datos.";
            }
            else if (opcion == 2)
            {
                label1.Text = "En la ventana Respaldo de ventas se pueden respaldar los datos de la base de datos. " +
                             "Por fecha: puedes hacer una consulta de la fecha que desees y puedes respaldarla. " +
                             "Todo: se respalda toda la tabla de ventas." +
                             "En la parte de restaurar el usuario al presionar el botón de “Restaurar” los datos de la tabla principal Ventas se eliminará y se insertan los de la tabla restaurar de la base de datos Restaurar.";
            }
            else if (opcion == 3)
            {
                label1.Text = "La ventana “Acerca de”. " +
                              "Muestra los datos Pertientes. Como el nombre de la Universidad en la que se desarrollo el software." +
                                "El nombre del desarrollador. " +
                            "La materia en la que se desarrollo." +
                             "Y claro el nombre la empresa a la que se desarrollo este software.";
            }
            else if (opcion==4)
            {
                label1.Text = "A esta Ventana solo pueden entrar los Usuarios que son empleados. " +
"En el botón buscar se abre una nueva ventana en la que puedes hacer la consulta de los productos en stock." +
"Una vez que el código este en la caja de texto al presionar el botón “Agregar” los datos de ese código se pasan a la tabla de ventas para capturar su venta. " +
"Botón Mas: Al seleccionar una fila de la tabla y presionar este botón es para el aumento de ese producto en cantidad. " +
"Botón Menos: Al seleccionar una fila de la tabla y presionar este botón se disminuye la cantidad del producto al llegar a cero la cantidad, el registro de esta venta se borra de la tabla. " +
"Al vender los datos de esta tabla se almacenan en la base de datos con sus datos y la fecha de la venta.";
            }
        }


    }
}
