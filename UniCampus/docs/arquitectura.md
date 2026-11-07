# Diagrama de arquitectura (tres capas + MVP)

```mermaid
flowchart LR
  subgraph Presentacion["Presentación (UniCampus.Web)"]
    V["Vista<br/>Actividades.aspx<br/>IActividadesView"] --> P["Presenter<br/>ActividadesPresenter"]
  end
  subgraph Negocio["Negocio (UniCampus.BLL)"]
    S["ActividadService<br/>validaciones y reglas"]
  end
  subgraph Datos["Datos (UniCampus.DAL)"]
    R["ActividadRepository<br/>ADO.NET parametrizado"]
  end
  DB[("SQL Server<br/>UniCampusDB")]
  P --> S --> R --> DB
  DB -. resultado .-> R -. DTO .-> S -. resultado .-> P -. mensaje y listado .-> V
```

Dependencias (hacia adentro): Web -> BLL -> DAL -> SQL Server. `UniCampus.Entities` es referenciado por las tres capas.
Pegar este bloque en https://mermaid.live para exportar la imagen (PNG) y usarla en el informe.
