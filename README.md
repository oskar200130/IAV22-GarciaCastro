# GRANJA FRIJOLES

Andrés Carnicero Esteban
Alberto Muñoz Fernández
Sergio Molinero Aparicio
Oscar García Castro

El proyecto consistirá en una escena con agentes inteligentes(estilo práctica del fantasma de la ópera) haciendo cada
participante del grupo la IA de diferentes agentes como:

Para este proyecto, realizaremos en grupo un escenario con varias zonas en las que habrá varios agentes inteligentes que seguirán rutinas y reaccionarán 
a eventos que ocurran de forma dinámica.

Como se expone en el título, será una granja, en la que tendremos a los siguientes agentes: un granjero, un perro, unas ovejas, un huerto y un lobo.
Además, habrá una gallina, que la moverá el jugador y podrá interactuar con varios elementos del escenario.

Ahora explicaremos brevemente el propósito general de los agentes y el escenario, después entraremos en más detalle con los respectivos diagramas y esquemas, de
árboles y máquinas de estados que usaremos para cada agente.

## RESUMEN
La granja estará distribuida en varias zonas, la casa del granjero, la zona del huerto, el recinto de ovejas, la zona de pasto, la mesa de la comida, 
el establo de cerdos, la caseta del perro y la cueva del lobo.

En la granja tendremos un ciclo día/noche y cada agente deberá seguir una rutina en cada franja horaria.

### -Día
Por el día ocurrián los siguientes eventos de forma secuencial.

Por ejemplo, el granjero se levantará cada mañana, irá a cuidar el huerto, abrir el corral de las ovejas para que pasten, se irá a comer, cerrará a las ovejas,
dará de comer a los cerdos y se irá a dormir.

Las ovejas merodearán en manada por su recinto y saldrán a pastar cuando el granjero las abra, siguiendo la ruta que marque el perro, el perro además tendrá
que tener cuidado por si se escapa alguna oveja ir a buscarla.

Cuando el perro no esté pendiente de las ovejas, deambulará por su caseta.

El lobo por el día simplemente dormirá en su cueva sin intervenir en la granja.

Y finalmente tenemos al jugador, a la gallina, que, como hemos mencionado antes, podrá interactuar con el escenario para modificar las rutinas del resto de agentes.
En concreto podrá pisotear el huerto para que le granjero corte lo que esté haciendo y tenga que ir a arreglarlo, abrirá la puerta de las ovejas para que alguna se pueda escapar y el perro tenga que ir a por ella y podrá esconder la comida de los cerdos (bloques de paja) para que el granjero tenga que ir a buscarla antes de darles de comer.

### -Noche
Por la noche las cosas cambian un poco.

El granjero y el perro se van a dormir, pero el lobo se despierta.

El objetivo del lobo como era obvio, será comerse a las ovejas, se encargará durante toda la noche de colarse en el recinto de ovejas, se llevará una y si llega con
ella hasta su cueva se la comerá, la gallina le puede ayudar abriendo la puerta para que se escapen y las encuentre más fácilmente. Las ovejas a su vez intentarán
huir del lobo si está cerca, aunque servirá de poco.

No obstante, la gallina puede ayudar a salvar a las ovejas, por la noche, podrá hacer ruido para despertar, tanto al granjero como al perro.
Si despierta al perro, este perseguirá a la gallina, así ella podrá llevarlo hasta el lobo, entonces el perro perseguirá al lobo y si lo intercepta, este se irá corriendo
y el perro volverá con la oveja.

El granjero funciona parecido, la gallina tendrá que hacer ruido en más sitios para despertarlo, pero cuando lo haga, este correrá de manera automática hacia el lobo, 
sin tener que seguir a la gallina, y después interceptará al lobo de la misma manera que el perro.

## DIAGRAMAS EN DETALLE
### Escenario
Aquí explicaremos con la ayuda de un diagrama, cómo está distribuído el mapa, para implementarlo por zonas usaremos el NavMesh que proporciona Unity (igual que en la práctica del fantasma de la ópera).

![alt text](https://raw.githubusercontent.com/oskar200130/IAV22-GarciaCastro/main/ImagenesDocu/DiagramaMapa.png)

Todas las areas están conectadas directamente, los únicos puntos críticos són las vallas que separan la zona de la granja del campo de pastar, pues sólo se puede acceder por esas 2 puertas.

También hay que tener en cuenta de que no todos los agentes pueden acceder a todas las zonas, el perro, le granjero y la gallina sí que pueden, incluyendo el recinto
de ovejaas. Sin embargo, las ovejas y el lobo sólo pueden ir del recinto al campo (y cueva), aunque se escapen las ovejas y las puertas de las vayas de fuera estén siempre abiertas, no pasan a la zona de la granja.

Por otro lado, el la leyenda de la derecha se ven los puntos de interacción que puede usar la gallina, los de sonido y la puerta están fijos siempre, pero los de comida se pueden desplazar pues son la comida de los cerdos, aunque al comenzar cada día volverán a aparecer en su zona inicial.  
Respecto a los puntos de sonido, los 3 de arriba son los necesarios para desperar al granjero por la noche, el de abajo es exclusivamente para el perro.

Faltaría en ese diagrama poner un punto en el huerto, donde la gallina puede pisarlo.

## Granjero
Como ya hemos dicho antes, el granjero será el agente más complejo, seguirá una rutina bastante larga durante el día que podrá interrumpir la gallina desde diferentes puntos y por la noche dormirá pero estará alerta por el lobo.

Las acciones de la rutina serán las siguientes:

### + Día +
    
    - Se levanta (sale da la casa).
    - Va a cuidar el huerto, regarlo si está seco o repararlo y posteriormente reagarlo si por la noche la gallina lo ha pisoteado.
    - Va a abrir el recinto de las ovejas para que el perro las lleve a pastar.
    - Hace un parón de unos segundos para comer (va a la mesa del medio).
    - Vuelve al recinto de ovejas para cerrarlo.
    - Se acerca al establo de los cerdos para darlos de comer, si la gallina se ha llevado la comida tendrá que ir a buscarla.
    - Se va a dormir (entra en casa).
    
    La gallina puede interrumpir estas acciones de manera inmediata:
    - Si la gallina pisotea el huerto, el granjero volverá al huerto a arreglarlo y luego seguirá con la tarea que tenga que realizar.
    - Cuando vaya a alimentar a los cerdos, la comida puede estar en el sitio habitual o en otro sitio porque la ha movido la gallina, en ese caso, 
    deberá ir a buscar la comida antes de darles de comer.
    - Si se escapa una gallina, cuando el perro venga de rescatarla, el granjero deberá ir a cerrar otra vez la puerta del recinto.
    
### + Noche +
    
    Mientras duerme, el granjero se podrá despertar si la gallina hace ruido, en concreto si activa los 3 generadores 
    de ruido alrededor de la casa, cuando ocurra, pueden pasar 3 cosas:
    
    - Si el lobo está en el recinto de las ovejas o directamente ya se ha llevado una, el granjero correrá de manera automática hacia donde esté el lobo.
    Cuando intercepta al lobo, devuelve la oveja de la mano al recinto y si la puerta está abierta (por la gallina), la vuelve a cerrar.
    - Si el lobo no está en el recinto, irá a cerrar la puerta de las ovejas si está abierta.
    - Si no está abierta ni está el lobo, entonces se volverá a meter a casa.
    
### Implementación
Para implementar estos comportamientos, hemos decidido usar un árbol de comportamientos, aquí proponemos una solucíon:
![alt text](https://github.com/oskar200130/IAV22-GarciaCastro/blob/main/ImagenesDocu/GranjeroDT.png)


## Huerto
El huerto será el agente más simple, podrá tener 3 estados, sin regar, regado y pisoteado.

    - Al inicio, lo normal es que el huerto esté "Sin regar".
    - El granjero deberá venir a regarlo para pasarlo al estado "Regado".
    - Se mantiene todo el día y la noche en "Regado", hasta que empieza el nuevo día y vuelve a "Sin regar". 
    
    La gallina puede interrumpir estos estados:
    - Si la gallina se acerca a pisotear el huerto, independientemente del estado en el que esté, pasará a estado "Pisoteado".
    - Cuandoel granjero vaya a arreglarlo, el huerto pasará al estado en el que se encontraba anteriormente.
    
    
### Implementación
Para implementar el huerto hemos propuesto una solucíon por máquina de estados, pues es más sencillo de implementar:
![alt text](https://raw.githubusercontent.com/oskar200130/IAV22-GarciaCastro/main/ImagenesDocu/HuertoSM.png)


## Ovejas 
Las ovejas serán agentes que tengan tanto comportamientos individuales como en manada.

Para empezar, sus zonas de movimiento a diferencia del resto de agentes, estarán restringidas al propio recinto y al campo de pasto,
además de la cueva del lobo. No podrán pasar la valla que conecta con el resto de la granja.

### + Día +
    - El estado por defecto de cada oveja será estar dentro del recinto merodeando con el resto de ovejas.
    Aquí se usará un algoritmo que tenga en cuenta el resto de ovejas, para crear un comportamiento en manada.
    - Si es la hora de salir a pastar, las ovejas, también en manada, seguirán al perro por una ruta establecida en el campo de pastar.
    Saldrán todas del recinto y cuando acabe la ruta, seguirán al perro hasta dentro de nuevo.
    
    La gallina puede interrumpir:
    - Si cuando están dentro del recinto, la gallina abre la puerta, la oveja más cercana a la puerta saldrá del recinto y escapará de él.
    - Si cuando pasa eso, el perro intercepta a la oveja, esta pasará a ser hija del perro y la llevará de nuevo al recinto,
    posteriormente el granjero cerrará la puerta.
    
### + Noche +
    Ahora entra el factor lobo:
    
    - Las ovejas tendrán el mismo comportamiento que de día, exceptuándo el salir a pastar en manada.
    - Si el lobo se acerca lo suficiente a una oveja, tanto dentro como fuera del recinto, esta intentará escapar, aunque sin mucho éxito, pues
    son claramente más lentas que el lobo.
    - Cuando el lobo la alcanza, esta pasará a ser su "hijo" y se la llevará capturada a la cueva, si el lobo consigue llegar con ella a la cueva, la 
    oveja pasará al estado "Muerta", efectivamente se la ha comido PEGUI+18.
    - En las manos del lobo podrán ser salvadas tanto por el perro como por el granjero, trayéndolas de la misma manera al recinto.
    
### Implementación
También hemos pensado en una máquina de estados:
![alt text](https://raw.githubusercontent.com/oskar200130/IAV22-GarciaCastro/main/ImagenesDocu/OvejaSM.png)


## Lobo 
El lobo será un agente que sólo trabaja de noche, por el día duerme en su cueva.

    - Al comenzar la noche, el lobo buscará la oveja más cercana por si hay alguna escapada por el campo, si no hay ninguna
    irá directamente al recinto (cuando llega a la valla del recinto la salta).
    - Al interceptar la oveja que busca, la coge y se la lleva a su cueva, pero con mucha menos velocidad.
    
    El granjero y el perro pueden interrumpir sus ataques, estos pueden ir a por el lobo cuando se despiertan, si el lobo está lo suficientemente
    cerca del recinto o directamente ha capturado una oveja:
    - Si el lobo está cerca pero sin oveja, cuando cualquiera de los otros 2 agentes estén cerca suyo, el lobo volverá despavorido a su cueva.
    - Si el lobo está con la oveja, primero se la tendrán que quitar de las manos y después huirá despavorido.
    
### Implementación
También hemos pensado en una máquina de estados:
![alt text](https://raw.githubusercontent.com/oskar200130/IAV22-GarciaCastro/main/ImagenesDocu/LoboSM.png)


## Perro 
El último agente que queda por comentar paso a paso es el perro.

### + Día +
    - Durante el día, el estado normal del perro será estar merodeando por su caseta.
    - Este estado se interrumpirá en 2 ocasiones:
        - Si es la hora de pastar y el granjero abre la puerta de las ovejas, el perro deberá ir a la puerta del recinto para atraer a todas las ovejas 
        y darles un paseo por el campo (será una ruta preestablecida), una vez acaba esa ruta, vuelve al recinto con todas las ovejas y las vuelve a dejar.
        Luego vuelve a su caseta.
        - Si se escapa una oveja, el perro va corriendo a por ella, cuando la intercepta, la coge y la mete de vuelta al recinto.
        
### + Noche + 
    Durante la noche, el perro actúa parecido al granjero, duerme y no se despierta a no ser que la gallina active el punto de ruido que hay al 
    lado de su caseta:
    - Cuando se despierta, inmediatamente el perro se pone a perseguir a la gallina (no ocurre nada si se chocan).
    - El anterior comportamiento dura alrededor de 10-15 segundos, si se pasa el tiempo y no ha ocurrido nada, el perro vuelve a la caseta.
    - Sin embargo, si durante ese tiempo, la gallina lleva al perro hasta el lobo, lo hará huir si no tiene a ninguna oveja o lo perseguirá hasta rescatarla,
    después traerá la oveja de vuelta.
    - Si no consigue interceptar al lobo antes de llegar a la cueva, como la oveja muere, el perro se vuelve a la caseta.
    
### Implementación
También hemos pensado en una máquina de estados:
![alt text](https://raw.githubusercontent.com/oskar200130/IAV22-GarciaCastro/main/ImagenesDocu/perroSM.png)

## Requisitos
A - Tener un NavMesh por zonas funcional, por el que se puedan mover los diferentes agentes, con sus respectivas restricciones.
B - Tener un ciclo día-noche que haga que los agentes cambies sus rutinas.
C - El granjero cumple sus rutinas y reacciona a las interrupciones de la gallina.
D - El perro cumple sus rutinas y reacciona a las interrupciones de la gallina.
E - Las ovejas cumplen sus rutinas y reaccionan a las interrupciones de la gallina.
F - El lobo cumple sus rutinas y reacciona a las interrupciones del perro y granjero.
G - La gallina es capaz de interactuar con el entorno cambiando los comportamientos de los demás agentes.
H - El proyecto tiene un sistema de debug para observar a los diferentes agentes por teclas.


## REFERENCIAS Y ASSETS
https://sketchfab.com/3d-models/chicken-rigged-6e3b93c078114c52bfe4cfa08b9843eb
https://sketchfab.com/3d-models/low-poly-sheep-20ef523bee784e4a939b4c5f8734edfc
https://sketchfab.com/3d-models/farmer-low-poly-2d913738203a49dfb4b30e7f4633e75f
https://sketchfab.com/3d-models/werewolf-451d0af45af74892b119eabed444fa04
https://sketchfab.com/3d-models/dog-2d69b8134a2e43888ee4fbef224763b4
https://sketchfab.com/3d-models/barn-528b7b55db9b47e0a8129b50717f0cdd#download



