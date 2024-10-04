# PulseSpam: Aplicación para Gestión de Encuestas

## Sobre el proyecto

Versión pública para la visualización del código del Trabajo de Fin de Grado "PulseSpam: Aplicación para Gestión de Encuestas" para acceder al Grado en Ingeniería Informática.

Disponible en https://repositorio.unican.es/xmlui/handle/10902/30032

## Descripción del sistema

PulseSpam es un sistema conformado por una aplicación de escritorio y una aplicación web que busca poder gestionar la satisfacción de los miembros de una empresa a través de la realización de una pregunta diaria, similar a un sistema de encuestas. Está pregunta diaria aparece de forma automática en el equipo de cada miembro y las respuestas recibidas se computan y se gestionan a través de una aplicación web que sirve de central para el administrador del sistema, desde donde pueden comprobarse los datos y estadísticas mediante distintas gráficas y filtros, añadir nuevas preguntas y programarlas a futuro, entre otras funciones.

Para ello, el sistema cuenta con cuatro elementos:
* Una base de datos no relacional en la que se aloja toda la información del sistema
* Una API REST que permite a las aplicaciones acceder a dicha base de datos
* Una aplicación de escritorio, diseñada como un servicio automático de Windows
* Una aplicación web, que sirve como *dashboard* desde el que gestionar al sistema

## Tecnologías utilizadas

| Elemento 	                  | Tecnología    	                                |
|-----------------------------|-----------	|
| Aplicación web      	      | React, MaterialUI, Typescript, Javascript 	    |
| Aplicación de escritorio    | WPF (Windows Presentation Foundation), XML, C#  |
| Servicio REST             	| ASP.NET Core, C#         	                      |
| Base de datos             	| MongoDB         	                              |

## Detalle de funcionalidades del sistema

El proyecto está dividido tres secciones principales, cada una con un conjunto de subobjetivos o funcionalidades que deben ser cubiertas:
- Backend (Base de datos NoSQL, servicio web):
  - Permitir la gestión a través de operaciones CRUD de los principales elementos en el sistema (preguntas, respuestas, categorías, etc.)
  - Controlar la programación de las preguntas, asegurando la existencia de un máximo de una pregunta por cada día laboral.
  - Anonimizar las respuestas de los usuarios.
  - Asegurar que cada usuario puede responder la pregunta diaria una única vez.
- Frontend (Aplicación web para gestión y administración):
  - Permitir la visualización sencilla de los datos asociados a las respuestas de un día determinado, a través de un gráfico resumen.
  - Gestión de usuarios: dar de alta y baja usuarios con distintos permisos
(administrador o usuario).
  - Permitir el acceso únicamente a los administradores.
  - Programación de tareas de preguntas en un rango de fechas dado.
  - Creación y gestión de preguntas y categorías.
- Frontend (Aplicación de escritorio):
  - Lanzarse de forma automática todos los días en el mismo horario.
  - Permitir al usuario responder la pregunta diaria, aplazarla a un horario posterior dentro del mismo día o declinar responderla.
  - Controlar el acceso a la aplicación a través de un sistema de login, permitiendo el acceso únicamente a los usuarios básicos
