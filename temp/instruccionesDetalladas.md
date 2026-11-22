# Instrucciones

En esta entrega se aplica la integración continua: se implementa Jenkins como gestor de operaciones sobre los dos contenedores que construyeron en la Entrega 1. Son 150 puntos:

1. Documento con las características requeridas para implementar Jenkins (25 pts): requisitos de hardware y software, cómo lo instalaron y por qué (se recomienda el contenedor jenkins/jenkins:lts con un volumen), puertos, plugins (Git/GitHub, Pipeline, Docker Pipeline) y credenciales.
2. Instalación y configuración de Jenkins (20 pts): Jenkins funcionando, con los plugins y las credenciales guardadas en Jenkins (nunca escritas en el código).
3. Pipeline como código (25 pts): un Jenkinsfile en el repositorio con las etapas Checkout, Build y Test, y un bloque post. Un job armado solo en la interfaz vale menos.
4. Integración con GitHub (15 pts): el pipeline se dispara solo con cada push, con webhook o con Poll SCM (las dos opciones valen lo mismo).
5. Construcción de los contenedores de la Entrega 1 (20 pts): el pipeline construye las imágenes de los dos servicios.
6. Pruebas y verificación automática (15 pts): pruebas unitarias o una prueba que levante el compose y verifique que los dos contenedores se comunican por el nombre del servicio; si algo falla, el build debe fallar.
7. Evidencias de ejecución (15 pts): vista de etapas, salida de consola e historial de builds; idealmente un build que falla y el que lo corrige.
8. Calidad del documento (15 pts): integrantes, diagrama del flujo (commit → GitHub → Jenkins → build → pruebas), enlace al repositorio, conclusiones propias y referencias en APA.

Cada criterio se califica en cuatro niveles: Excelente (100 %), Bueno (75 %), Básico (50 %) e Insuficiente (25 % o menos). Por ejemplo, si Jenkins toma el código de GitHub pero los builds solo se lanzan a mano, la integración queda en Básico.

Importante: quienes ya tienen GitHub Actions en el repositorio pueden dejarlo, pero en esta entrega se califica Jenkins, que es lo que pide el enunciado.

Un consejo: tomen capturas a medida que avanzan; la evidencia paso a paso es lo que más pesa. 