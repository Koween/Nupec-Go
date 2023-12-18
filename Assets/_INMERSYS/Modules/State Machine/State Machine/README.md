
# Máquina de estados

## Anatomía del inspector

<img width="1000" alt="Captura de pantalla 2023-07-30 a la(s) 15 36 52" src="https://github.com/inmersysmx/Unity-Modules/assets/115565120/302742ea-aad7-44a6-9531-a1da063d4a28">

<img width="926" alt="Captura de pantalla 2023-07-30 a la(s) 15 41 27" src="https://github.com/inmersysmx/Unity-Modules/assets/115565120/2c38bcbe-c13a-41d3-b7a9-6ea7ce3f473a">

# Features

## Omitir o saltar un estado

<img width="834" alt="image" src="https://github.com/inmersysmx/Unity-Modules/assets/115565120/ff28ff9e-0bf6-4083-b277-99df3745767d">

- None = No omitir el estado actual.
- Ignore Skip Events: Omite el estado actual sin ejecutar los eventos de OnSkip.
- Invoke Skip Events: Omite el estado actual ejecutando los eventos de OnSkip.

## Establecer una cuenta regresiva o Delay

Inicia una cuenta regresiva, y al finalizar ejecuta los estados de OnStart, OnComplete, o pasa al siguiente estado.

<img width="826" alt="image" src="https://github.com/inmersysmx/Unity-Modules/assets/115565120/e1cc4e73-fa80-41e7-bddf-9ff0f294ef0d">

- Before Start: Inicia la cuenta regresiva antes de empezar el estado (antes de ejecutar los eventos de OnStart).
- To Finish: Al iniciar el estado, ejecuta los eventos de OnStart e inicia la cuenta regresiva para entonces ejecutar los eventos de OnComplete.
- To Continue: Una vez que se marca el estado como Completado, ejecuta los eventos de OnComplete e inicia la cuenta regresiva para entonces pasar al siguiente estado o máquina de estados.

Para ejecutar cualquier tipo de Timer, es necesario que el valor de Timing sea mayor a 0.

## Clases objeto asociadas

La arquitectura base de esta clase está diseñada para que funja como un Event wrapper sencillo (Las fases de los estados).

Para asociar cualquier clase objeto a la máquina de estados:
- La nueva clase objeto deberá heredar de la clase StateObject.
- Se deberá agregar como referencia al campo StateObject desde el inspector a mínimo un estado.

### Anatomía

<img width="854" alt="Captura de pantalla 2023-07-30 a la(s) 15 54 01" src="https://github.com/inmersysmx/Unity-Modules/assets/115565120/c5e1c9be-5c63-4d2b-a372-0f871f4d572a">

En algunos casos es necesario ejecutar eventos durante la primer interacción con un objeto debido a que éstos podrían usarse en múltiples ocasiones. Para estos casos la opción de
First Interaction Mode podría ayudar.

<img width="649" alt="image" src="https://github.com/inmersysmx/Unity-Modules/assets/115565120/f8763ae6-199f-4673-abed-ca7b4b0f3b70">

- None = No ejecutar eventos de OnFirstInteraction o de OnCompleteFirstInteraction en la primer interacción.
- Override State Events: Ejecuta los eventos de OnFirstInteraction y no los de OnStart la primera vez que se interactúa con el objeto.
- Run State Events: Ejecuta los eventos de OnFirstInteraction y los de OnStart la primera vez que se interactúa con el objeto.

