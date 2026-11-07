<%@ Page Language="C#" %>
<%@ Import Namespace="UniCampus.BLL" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        // Solo visible desde el mismo equipo que ejecuta el servidor.
        if (!Request.IsLocal) { Response.StatusCode = 404; Response.End(); return; }
        salida.InnerText = ActividadService.Diagnosticar();
    }
</script>
<!DOCTYPE html>
<html>
<head><meta charset="utf-8" /><title>Diagnóstico de conexión</title></head>
<body style="font-family: Arial; margin: 30px;">
    <h2>Diagnóstico de conexión - UniCampus</h2>
    <pre id="salida" runat="server"></pre>
    <p><a href="Actividades.aspx">Ir a Actividades</a></p>
</body>
</html>
