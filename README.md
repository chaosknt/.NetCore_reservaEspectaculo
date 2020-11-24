# reservaEspectaculo | Requerimientos

<h4>Objetivos 📋</h4>

<p>
Desarrollar un sistema, que permita la administración general del cine (de cara a los empleados): 
Peliculas, Salas, unciones, Clientes, etc., como así también, permitir a los clientes, realizar reserva de las funciones ofrecidas. 
Utilizar Visual Studio 2019 preferentemente y crear una aplicación utilizando ASP.NET  MVC Core (versión a definir por el docente 2.2 o 3.1).
</p>

<h4>Proceso de ejecución en alto nivel ☑️ 📢</h4>

<p>
<ul class="__web-inspector-hide-shortcut__">
<li>Crear un nuevo proyecto en <a href="https://visualstudio.microsoft.com/en/vs/" rel="noopener noreferrer" target="_blank">visual studio</a> <span class="fabric-icon ms-Icon--NavigateExternalInline font-size" role="presentation" aria-hidden="true"> </span>.</li>
<li>Adicionar todos los modelos dentro de la carpeta Models cada uno en un archivo separado.</li>
<li>Especificar todas las restricciones y validaciones solicitadas a cada una de las entidades. <a href="https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1" rel="noopener noreferrer" target="_blank">DataAnnotations</a> <span class="fabric-icon ms-Icon--NavigateExternalInline font-size" role="presentation" aria-hidden="true"> </span>.</li>
<li>Crear las relaciones entre las entidades</li>
<li>Crear una carpeta Data que dentro tendrá al menos la clase que representará el contexto de la base de datos DbContext.</li>
<li>Crear el DbContext utilizando base de datos en memoria (con fines de testing inicial). <a href="https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-3.1" rel="noopener noreferrer" target="_blank">DbContext</a> <span class="fabric-icon ms-Icon--NavigateExternalInline font-size" role="presentation" aria-hidden="true"> </span>, <a href="https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=vs" rel="noopener noreferrer" target="_blank">Database In-Memory</a> <span class="fabric-icon ms-Icon--NavigateExternalInline font-size" role="presentation" aria-hidden="true"> </span>.</li>
<li>Agregar los DbSet para cada una de las entidades en el DbContext.</li>
<li>Crear el Scaffolding para permitir los CRUD de las entidades al menos solicitadas en el enunciado.</li>
<li>Aplicar las adecuaciones y validaciones necesarias en los controladores.</li>
<li>Realizar un sistema de login con al menos los roles equivalentes a &lt;Usuario Cliente&gt; y &lt;Usuario Administrador&gt; (o con permisos elevados).</li>
<li>Si el proyecto lo requiere, generar el proceso de registración.</li>
<li>Un administrador podrá realizar todas tareas que impliquen interacción del lado del negocio (ABM "Alta-Baja-Modificación" de las entidades del sistema y configuraciones en caso de ser necesarias).</li>
<li>El &lt;Usuario Cliente&gt; sólo podrá tomar acción en el sistema, en base al rol que tiene.</li>
<li>Realizar todos los ajustes necesarios en los modelos y/o funcionalidades.</li>
<li>Realizar los ajustes requeridos del lado de los permisos.</li>
<li>Todo lo referido a la presentación de la aplicaión (cuestiones visuales).</li>
</ul>
</p>

<h4>Entidades 📄</h4>
<p>
<ul>
<li>Usuario</li>
<li>Cliente</li>
<li>Empleado</li>
<li>Reserva</li>
<li>Función</li>
<li>Pelicula</li>
<li>Sala</li>
<li>Genero</li>
</ul>

Importante: Todas las entidades deben tener su identificador unico. Id o <ClassNameId>

Las propiedades descriptas a continuación, son las minimas que deben tener las entidades.
Uds. pueden agregar las que consideren necesarias. De la misma manera Uds. deben definir los tipos de datos asociados a cada una de ellas, 
como así también las restricciones.
</p>


<p>

<h5>Usuario<h5>
<ul>
<li>Nombre</li>
<li>Email</li>
<li>FechaAlta</li>
<li>Password</li>
</ul>

<h5>Cliente<h5>

<ul>
<li>Nombre</li>
<li>Apellido</li>
<li>DNI</li>
<li>Telefono</li>
<li>Direccion</li>
<li>FechaAlta</li>
<li>Email</li>
<li>Reservas</li>
<li></li>
</ul>

<h5>Empleado<h5>

<ul>
<li>Nombre</li>
<li>Apellido</li>
<li>DNI</li>
<li>Telefono</li>
<li>Direccion</li>
<li>FechaAlta</li>
<li>Email</li>
<li>Legajo</li>
</ul>
 
<h5>Pelicula<h5>

<ul>
<li>FechaLanzamiento</li>
<li>Titulo</li>
<li>Descripcion</li>
<li>Genero</li>
<li>Funciones</li>
</ul>

<h5>Genero<h5>

<ul>
<li>Nombre</li>
<li>Peliculas</li>
</ul>

<h5>Sala<h5>
<ul>
<li>Numero</li>
<li>TipoSala</li>
<li>CapacidadButacas</li>
<li>Funciones</li>
</ul>
 
<h5>TipoSala<h5>

<ul>
<li>Nombre</li>
<li>Precio</li>
</ul>

<h5>Función<h5>

<ul>
<li>Fecha</li>
<li>Hora</li>
<li>Descripcion</li>
<li>ButacasDisponibles</li>
<li>Confirmada</li>
<li>FechaAlta</li>
<li>Pelicula</li>
<li>Sala</li>
<li>Reservas</li>
</ul>

<h5>Reserva<h5>
<ul>
<li>Funcion</li>
<li>FechaAlta</li>
<li>Cliente</li>
<li>CantidadButacas</li>
</ul>
NOTA: aquí un link para refrescar el uso de los Data annotations .
</p>

<h4>Caracteristicas y Funcionalidades ⌨️</h4>

<p>Todas las entidades, deben tener implementado su correspondiente ABM, a menos que sea implicito el no tener que soportar alguna de estas acciones.</p>
<p>
<h5>Usuario</h5>

<ul>
<li>Los clientes pueden auto registrarse.</li>
<li>La autoregistración desde el sitio, es exclusiva para los clientes. Por lo cual, se le asignará dicho rol.</li>
<li>Los empleados, deben ser agregados por otro Empleado.
<ul>
<li>Al momento, del alta del empleado, se le definirá un username y password.</li>
<li>También se le asignará a estas cuentas el rol de empleado.</li>
</ul>
</li>
</ul>

<h5>Cliente</h5>

<ul>
<li>Un cliente puede realizar una reserva Online
<ul>
<li>El proceso será en modo Wizard.
<ul>
<li>Selecciona la pelicula</li>
<li>Selecciona una fecha, dentro de los proximos 7 dias y la cantidad de butacas que quiere reservar.</li>
<li>Seleccionará una función disponible dentro de la oferta.
<ul>
<li>La oferta estará restringida desde el momento de la consulta hasta 7 dias posteriores.</li>
<li>Las funciones deben estar confirmadas</li>
<li>No debe incluir desde luego funciones que no tenga butacas disponibles.</li>
<li>Debe ser en base a la oferta de la pelicula seleccionada.</li>
<li>El cliente, solo puede tener una reserva activa.</li>
</ul>
</li>
<li>El cliente, podrá en todo momento, ver si tiene o no una reserva para una función futura.
<ul>
<li>Podrá cancelarla, solo si es hasta 24hs. antes.</li>
</ul>
</li>
</ul>
</li>
</ul>
</li>
<li>Puede ver las reservas pasadas.</li>
<li>Puede actualizar datos de contacto, como el telefono, dirección,etc.. Pero no puede modificar su DNI, Nombre, Apellido, etc.</li>
</ul>

<h5>Empleado</h5>
<ul>
<li>El empleado puede listar las reservas por cada función "en el futuro" o "en el pasado".</li>
<li>El empleado, puede habilitar o cancelar funciones.
<ul>
<li>Solo pueden cancelarse, si no tiene reservas.</li>
</ul>
</li>
<li>También, puede ver un balance de recaudación por pelicula en mes calendario.</li>
<li>Puede dar de alta las Salas, Peliculas, etc.
<ul>
<li>Nadie, puede eliminar las salas, pero si puede cambiar el tipo.</li>
</ul>
</li>
</ul>

<h5>Reserva</h5>
<ul>
<li>La reserva al crearse debe estar en estado activa.</li>
<li>El cliente solo puede tener una reserva activa.</li>
<li>La reserva, tiene que validar, que sea factible, en cuanto a la cantidad de butacas que selecciona al cliente para una función especifica.
<ul>
<li>Si puede realizar la reserva se debe actualizar las butacas disponibles (Capacidad de la sala vs Reservas realizadas previas y actual).</li>
</ul>
</li>
</ul>

<h5>Aplicación General</h5>

<ul>
<li>Información institucional.</li>
<li>Se deben listar las peliculas en cartelera.</li>
<li>Por cada pelicula, se tiene que poder listar las funciones activas para la proxima semana.</li>
<li>La disponibilidad de las funciones, solo puede verse al tener una sesión iniciada como cliente.</li>
</ul>

Nota: Las butacas no son numeradas. El complejo, no tiene limites fisicos en la construcción de salas. Las funciones tienen una duración fija de 2hs.
</p>

