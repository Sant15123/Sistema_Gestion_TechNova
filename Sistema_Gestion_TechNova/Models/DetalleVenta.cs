using MongoDB.Bson;

public class DetalleVentaItem
{
    public ObjectId ProductoId { get; set; }
    public string NombreProducto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal => Cantidad * PrecioUnitario;
}
