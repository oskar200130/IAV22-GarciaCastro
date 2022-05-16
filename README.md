--GRANJA CON NOMBRE PROVISIONAL--

Andrés Carnicero Esteban
Alberto Muñoz Fernández
Sergio Molinero Aparicio
Oscar García Castro

El proyecto consistirá en una escena con agentes inteligentes(estilo práctica del fantasma de la ópera) haciendo cada
participante del grupo la IA de diferentes agentes como:
    
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