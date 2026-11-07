<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Actividades.aspx.cs" Inherits="UniCampus.Web.Actividades" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>UniCampus | Actividades académicas</title>
    <meta charset="utf-8" />
    <style>
        body { font-family: Arial, sans-serif; margin: 30px; background: #f5f7fb; }
        .panel { max-width: 1100px; margin: 0 auto 20px; background: #fff; padding: 24px; border-radius: 10px; }
        h1, h2 { color: #17365d; } h2 { margin-top: 0; }
        .grid { display: grid; grid-template-columns: repeat(2, 1fr); gap: 14px; }
        .grid .full { grid-column: 1 / -1; }
        label { display: block; font-weight: bold; margin-bottom: 4px; }
        .campo { width: 100%; padding: 8px; box-sizing: border-box; }
        .btn { background: #17365d; color: #fff; border: 0; padding: 10px 20px; border-radius: 6px; cursor: pointer; margin-right: 8px; }
        .btn.sec { background: #6b7a90; }
        .tabla { width: 100%; border-collapse: collapse; }
        .tabla th, .tabla td { padding: 10px; border-bottom: 1px solid #ddd; text-align: left; }
        .tabla th { background: #17365d; color: #fff; }
        .ok { color: #1e7e34; } .error { color: #b02a37; }
        .mensaje { display: block; margin: 12px 0; font-weight: bold; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="panel">
        <h1>Registro de actividades académicas</h1>
        <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" EnableViewState="false" />

        <h2>Nueva actividad</h2>
        <div class="grid">
            <div class="full">
                <label for="txtTitulo">Título *</label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="campo" MaxLength="120" />
            </div>
            <div class="full">
                <label for="txtDescripcion">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="campo" TextMode="MultiLine" Rows="3" MaxLength="1000" />
            </div>
            <div>
                <label for="txtFechaInicio">Fecha y hora de inicio *</label>
                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="campo" TextMode="DateTimeLocal" />
            </div>
            <div>
                <label for="txtFechaFin">Fecha y hora de fin *</label>
                <asp:TextBox ID="txtFechaFin" runat="server" CssClass="campo" TextMode="DateTimeLocal" />
            </div>
            <div>
                <label for="txtLugar">Lugar</label>
                <asp:TextBox ID="txtLugar" runat="server" CssClass="campo" MaxLength="150" />
            </div>
            <div>
                <label for="txtCupos">Cupos disponibles *</label>
                <asp:TextBox ID="txtCupos" runat="server" CssClass="campo" />
            </div>
            <div>
                <label for="ddlEstado">Estado *</label>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="campo">
                    <asp:ListItem Text="Publicada" Value="Publicada" Selected="True" />
                    <asp:ListItem Text="Borrador" Value="Borrador" />
                    <asp:ListItem Text="Cancelada" Value="Cancelada" />
                    <asp:ListItem Text="Finalizada" Value="Finalizada" />
                </asp:DropDownList>
            </div>
        </div>
        <p>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn sec" OnClick="btnLimpiar_Click" CausesValidation="false" />
        </p>
    </div>

    <div class="panel">
        <h2>Listado de actividades</h2>
        <asp:GridView ID="gvActividades" runat="server" AutoGenerateColumns="False" CssClass="tabla"
            EmptyDataText="No hay actividades registradas.">
            <Columns>
                <asp:BoundField DataField="ActividadId" HeaderText="#" />
                <asp:BoundField DataField="Titulo" HeaderText="Actividad" />
                <asp:BoundField DataField="FechaInicio" HeaderText="Inicio" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="FechaFin" HeaderText="Fin" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Lugar" HeaderText="Lugar" />
                <asp:BoundField DataField="Cupos" HeaderText="Cupos" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>
        </asp:GridView>
    </div>
</form>
</body>
</html>
