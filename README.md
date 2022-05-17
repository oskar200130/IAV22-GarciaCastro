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
En concreto podrá pisotear el huerto para que le granjero corte lo que esté haciendo y tenga que ir a arreglarlo, abrirá la puerta de las ovejas para que alguna
se pueda escapar y el perro tenga que ir a por ella y podrá esconder la comida de los cerdos (bloques de paja) para que el granjero tenga que ir a buscarla antes de darles
de comer.

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

Todas las areas están conectadas directamente, los únicos puntos críticos són las vallas que separan la zona de la granja del campo de pastar, pues sólo se puede acceder por esas 2 puertas.

También hay que tener en cuenta de que no todos los agentes pueden acceder a todas las zonas, el perro, le granjero y la gallina sí que pueden, incluyendo el recinto
de ovejaas. Sin embargo, las ovejas y el lobo sólo pueden ir del recinto al campo (y cueva), aunque se escapen las ovejas y las puertas de las vayas de fuera estén siempre abiertas, no pasan a la zona de la granja.

Por otro lado, el la leyenda de la derecha se ven los puntos de interacción que puede usar la gallina, los de sonido y la puerta están fijos siempre, pero los de comida se pueden desplazar pues son la comida de los cerdos, aunque al comenzar cada día volverán a aparecer en su zona inicial.  
Respecto a los puntos de sonido, los 3 de arriba son los necesarios para desperar al granjero por la noche, el de abajo es exclusivamente para el perro.

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
    - Cuando vaya a alimentar a los cerdos, la comida puede estar en el sitio habitual o en otro sitio porque la ha movido la gallina, en ese caso, deberá ir       a buscar la comida antes de darles de comer.
### + Noche +
    
    Mientras duerme, el granjero se podrá despertar si la gallina hace ruido, en concreto si activa los 3 generadores de ruido alrededor de la casa, cuando       ocurra, pueden pasar 2 cosas:
    -Si el lobo está en el recinto de las ovejas o directamente ya se ha llevado una, el granjero correrá de manera automática hacia donde esté el lobo
    
    
    
    
    -Oveja:
        1-Pastan/Merodean
        2-Si abre establo se escapa
        3-Huyen del lobo si este se acerca
        4-Siguen al perro
    -Lobo:
        1-Ataca a las obejas por la noche
        2-Huye del granjero y del perro
        3-Se lleva a las ovejas -> Reduce velocidad
        4-Las suelta si el perro/granjero se le acercan
    -Perro:
        1-Cuando salen las ovejas a pastar las dirige
        2-Merodea en una zona durante el resto del día
        3-Duerme por la noche -> Se despierta si la gallina hace ruido
        4-Persigue al lobo o a la gallina
        5-Si ve a una obeja irse va a por ella y la trae de vuelta
    -Granjero:
        1-Se levanta
        2-Huerto
        3-Abrir/cerrar ovejas
        4-Paradiña para comer
        5-Alimentar a los cerdos/vacas
        6-Se duerme
        7-Si haces ruido en x sitios se levanta y va a por el lobo -> Si no hay lobo se vuelve a la cama
    -Gallina:
        1-Hace ruido
        2-Abrir puertas de día
        3-Robar comida de los animales
        4-Tirar la comida por el escenario
        5-Romper el huerto
    -Dia/Noche

La naveción y comportamientos de los agentes funcionarán con máquinas de estados y/o árboles de comportamiento, además 
de otros algoritmos del libro "AI for Games" de Ian Millington para que cada agente tenga comportamientos propios.

Todos los rasgos enumerados de los agentes no son completamente definitivos, podrían modificarse o incluso eliminarse
o ampliarse.
