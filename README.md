# 2022-2C-E-Historias-Clinicas
# Historias Clinicas 馃摉

## Objetivos 馃搵
Desarrollar un sistema de historias clinicas para un consultorio, que permita la administraci贸n y uso de esta. 
De cara a los empleados): Pacientes, Medicos, Empleados, HistoriaClinica, Episodio, Evoluciones, Epicrisis con Diagnostico, etc., como as铆 tambi茅n, permitir a los pacientes, realizar consultas acerca de su Historia clinica.
Utilizar Visual Studio 2019 preferentemente y crear una aplicaci贸n utilizando ASP.NET MVC Core (versi贸n a definir por el docente 3.1 o 6.0).

<hr />

## Enunciado 馃摙
La idea principal de este trabajo pr谩ctico, es que Uds. se comporten como un equipo de desarrollo.
Este documento, les acerca, un equivalente al resultado de una primera entrevista entre el paciente y alguien del equipo, el cual relev贸 e identific贸 la informaci贸n aqu铆 contenida. 
A partir de este momento, deber谩n comprender lo que se est谩 requiriendo y construir dicha aplicaci贸n, 

Deben recopilar todas las dudas que tengan y evacuarlas con su nexo (el docente) de cara al paciente. De esta manera, 茅l nos ayudar谩 a conseguir la informaci贸n ya un poco m谩s procesada. 
Es importante destacar, que este proceso, no debe esperar a ser en clase; es importante, que junten algunas consultas, sea de 铆ndole funcional o t茅cnicas, en lugar de cada consulta enviarla de forma independiente.

Las consultas que sean realizadas por correo deben seguir el siguiente formato:

Subject: [NT1-<CURSO LETRA>-GRP-<GRUPO NUMERO>] <Proyecto XXX> | Informativo o Consulta

Body: 

1.<xxxxxxxx>

2.< xxxxxxxx>


# Ejemplo
**Subject:** [NT1-A-GRP-5] Agenda de Turnos | Consulta

**Body:**

1.La relaci贸n del paciente con Turno es 1:1 o 1:N?

2.Est谩 bien que encaremos la validaci贸n del turno activo, con una propiedad booleana en el Turno?

<hr />

### Proceso de ejecuci贸n en alto nivel 鈽戯笍
 - Crear un nuevo proyecto en [visual studio](https://visualstudio.microsoft.com/en/vs/).
 - Adicionar todos los modelos dentro de la carpeta Models cada uno en un archivo separado.
 - Especificar todas las restricciones y validaciones solicitadas a cada una de las entidades. [DataAnnotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1).
 - Crear las relaciones entre las entidades
 - Crear una carpeta Data que dentro tendr谩 al menos la clase que representar谩 el contexto de la base de datos DbContext. 
 - Crear el DbContext utilizando base de datos en memoria (con fines de testing inicial). [DbContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-3.1), [Database In-Memory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=vs).
 - Agregar los DbSet para cada una de las entidades en el DbContext.
 - Crear el Scaffolding para permitir los CRUD de las entidades al menos solicitadas en el enunciado.
 - Aplicar las adecuaciones y validaciones necesarias en los controladores.  
 - Realizar un sistema de login con al menos los roles equivalentes a <Usuario Cliente> y <Usuario Administrador> (o con permisos elevados).
 - Si el proyecto lo requiere, generar el proceso de registraci贸n. 
 - Un administrador podr谩 realizar todas tareas que impliquen interacci贸n del lado del negocio (ABM "Alta-Baja-Modificaci贸n" de las entidades del sistema y configuraciones en caso de ser necesarias).
 - El <Usuario Cliente> s贸lo podr谩 tomar acci贸n en el sistema, en base al rol que tiene.
 - Realizar todos los ajustes necesarios en los modelos y/o funcionalidades.
 - Realizar los ajustes requeridos del lado de los permisos.
 - Todo lo referido a la presentaci贸n de la aplicai贸n (cuestiones visuales).
 
<hr />

## Entidades 馃搫

- Usuario
- Paciente
- Medico
- Empleado
- HistoriaClinica
- Episodio
- Evolucion
- Notas
- Epicrisis
- Diagnostico

`Importante: Todas las entidades deben tener su identificador unico. Id o <ClassNameId>`

`
Las propiedades descriptas a continuaci贸n, son las minimas que deben tener las entidades. Uds. pueden agregar las que consideren necesarias.
De la misma manera Uds. deben definir los tipos de datos asociados a cada una de ellas, como as铆 tambi茅n las restricciones.
`

**Usuario**
```
- Nombre
- Email
- FechaAlta
- Password
```

**Paciente**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Email 
- ObraSocial
- HistoriaClinica
```

**Medico**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Email
- Matricula
- Especialidad
```

**Empleado**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Email
- Legajo
```

**HistoriaClinica**
```
- Paciente
- Episodios
```

**Episodio**
```
- Motivo
- Descripcion
- FechaYHoraInicio
- FechaYHoraAlta
- FechaYHoraCierre
- EstadoAbierto
- Evoluciones
- Epicrisis
- EmpleadoRegistra
```

**Evolucion**
```
- Medico
- FechaYHoraInicio
- FechaYHoraAlta
- FechaYHoraCierre
- DescripcionAtencion
- EstadoAbierto
- Notas 
```

**Nota**
```
- Evolucion
- Empleado
- Mensaje
- FechaYHora
```

**Epicrisis**
```
- Episodio
- Medico
- FechaYHora 
- Diagnostico
```

**Diagnostico**
```
- Epicrisis
- Descripcion
- Recomendacion
```


**NOTA:** aqu铆 un link para refrescar el uso de los [Data annotations](https://www.c-sharpcorner.com/UploadFile/af66b7/data-annotations-for-mvc/).

<hr />

## Caracteristicas y Funcionalidades 鈱笍
`Todas las entidades, deben tener implementado su correspondiente ABM, a menos que sea implicito el no tener que soportar alguna de estas acciones.`

`IMPORTANTE: Ninguna entidad en el circuito de atenci贸n medica, puede ser modificado o eliminado una vez que se ha creado. Ej. No se puede Eliminar una Historia Clinica, No se puede modificar una nota de una evoluci贸n, etc.`

**Usuario**
- Los Pacientes pueden auto registrarse.
- La autoregistraci贸n desde el sitio, es exclusiva para los pacientes. Por lo cual, se le asignar谩 dicho rol.
- Los empleados, deben ser agregados por otro Empleado. Lo mismo, para los Medicos.
	- Al momento, del alta del empleado o medico, se le definir谩 un username y password.
    - Tambi茅n se le asignar谩 a estas cuentas el rol de empleado y/o medico seg煤n corresponda.

**Paciente**
- Un paciente puede consultar su historia clinica, con todos los detalles que la componen, en modo solo visualizaci贸n.
- Puede acceder a los episodios, y por cada episodio, ver las evoluciones que se tienen, con sus detalles.
- Puede actualizar datos de contacto, como el telefono, direcci贸n,etc.. Pero no puede modificar su DNI, Nombre, Apellido, etc.

**Empleado**
- Un empleado, puede modificar todos los datos de los pacientes. 
-- No puede quitar o asociar una nueva Historia Clinica a los pacientes.
- El Empleado puede listar todos los pacientes, y por cada uno, ver en sus detalles, la HistoriaClinica que tiene asociada y si tiene episodios abiertos. 
- El Empleado, puede crear un paciente, un empleado, y un medico. Cada uno de ellos, con su correspondientes datos requeridos y usuario.
- El Empleado, puede crear un Episodio para el Paciente, en la Historia Clinica del paciente.
-- Pero no puede hacer m谩s nada, que crearlo con su Motivo y Descripci贸n.

**Medico**
- Un Medico, puede crear evoluciones, en Episodios que esten en estado abierto.
-- Para ello, buscar谩 al paciente, acceder谩 a su Historia Clinica -> Episodio -> Crear la Evoluci贸n.
- Un medico puede cerrar una evluci贸n, si se han completado todos los campos. El campo de FechaYHoraCierre, se guardar谩 automaticamente. 
-- Un Empleado o Medico, pueden cargar notas en cada evoluci贸n seg煤n sea necesario.
-- Las notas pueden continuar agregandose, luego del cierre de la evoluci贸n.
- Puede cerrar un Episodio, pero para hacer esto, el sistema realizar谩 ciertas validaciones.

**HistoriaClinica**
- La misma se crea automaticamente con la creaci贸n de un paciente.
-- No se puede eliminar, ni realizar modificaciones posteriores.
-- El detalle internos de la misma, para los Medicos y empleados, pero dependiendo del rol, es lo que podr谩n hacer.
-- El paciente propietario de la HC, es el unico paciente que puede ver la HC.

- Por medio de la HC, se podr谩 acceder a la lista de Episodios, que tenga relacionados.

**Epidodio**
- La creaci贸n de un Episodio en una HC, solo puede realizarla un empleado.
-- El empleado, deberia acceder a un Paciente -> HC -> Crear Episodio, e ingresar谩:
--- Motivo. Ej. Traumatismo en pierna Izquierda.
--- Descripci贸n. Ej. El paciente se encontraba andando en Skate y sufri贸 un accidente.
- El episodio se:
-- Crear谩 en estadoAbierto automaticamente
-- Con una FechaYHoraInicio tambi茅n, de forma autom谩tica.
-- Con un Empleado, como el que cre贸 el episodio. (persona en recepci贸n, que recibe al paciente).

- Solo un medico puede cerrar un Episodio, para hacer esto, el sistema, validar谩:
-- 1. Que el Episodio, no tenga ninguna Evluci贸n en estado Abierta o no tenga evoluciones. Si fuese as铆, deber谩 mostrar un mensaje.
-- 2. Cargar谩 el Medico manualmente la FechaYHoraAlta (alta del episodio) del paciente.
-- 3. Le pedir谩 que cargue una Epicrisis, con su diagnostico y recomendaciones.
--- Una vez finalizado el diagnostico, el Episodio, pasar谩 a esatr en estado Cerrado.
-- 4. La FechaYHoraCierre, ser谩 cargada automaticamente, si se cumplen los requerimientos previos.

Nota: Si el cierre del episodio, es por la condici贸n sin evoluciones, se generar谩 un "Cierre Administrativo", en el cual, el sistema, cargar谩 una epicrisi, con alguna informaci贸n que el empleado ingresar谩 para dejar registro de que fue un cierre administrativo. Ej. El paciente realiz贸 el ingreso y antes de ser atendido, se fu茅. 

**Evolucion**
- Una evoluci贸n, solo la puede crear y gestionar un Medico.
-- La unica excepci贸n, es que un empleado, puede cargar notas en Evoluciones por cuestiones administrativas. Ej. Salvo, que el alta del paciente en la evoluci贸n, es 10/08/2020
- Automaticamente al crear una evoluci贸n se cargar谩:
-- Medico que la esta creando
-- FechaYHoraInicio
-- EstadoAbierto
-- FechaYHoraCierre (Cuando se registre el cierre)
- Manualmente:
-- La FechaYHoraAlta
-- DescripcionAtencion
-- Notas (Las que sean necesarias)

- Para cerrar una evoluci贸n, se deben haber cargado todos los datos manuales requeridos, y solo lo puede hacer un Medico.

**Nota**
- La nota pertenece a una evoluci贸n. 
-- Para crearla, uno solo puede hacerla desde una Evoluci贸n.
- En las notas, puede cargar un mensaje cualquier empleado o medico.
- Quedar谩 automaticmente la fecha y hora, como asi tambi茅n, quien es el que la carg贸.


**Epicrisis**
- La epicrisis, pertenes a un Episodio.
-- Solo puede haber una epicrisis por episodio.
-- Para poder crearla, todas las evoluciones, deben estar cerradas.
-- El Episodio debe estar abierto, y al finalizar este proceso, de estar todo ok, se debe cerrar automaticamente.
-- La epicrisis, solo debe poder cargarla un Medico.
-- La excepci贸n, es la creaci贸n automatica, si cierra un empleado, por proceso administrativo.
-- La FechayHora, se carga automaticamente
-- El Diagnostico, de forma Manual.

**Diagnostico**
- Pertenece a una Epicrisis. 
- Se cargar谩 una descripcion de forma manual
- Tambi茅n se cargar谩 una recomendacion.


**Aplicaci贸n General**
- Informaci贸n institucional.
- Se deben listar el cuerpo medico, junto con sus especialidades.
- Los accesos a las funcionalidades y/o capacidades, debe estar basada en los roles que tenga cada individuo.
