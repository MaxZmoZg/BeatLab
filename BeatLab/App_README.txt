Вы успешно добавили надстройку Office.

Чтобы воспользоваться функциями и стилями Office для данной HTML-страницы, добавьте следующие
ссылки в раздел <head> страницы, при необходимости изменив относительные пути:

    <!-- Ссылки Office: -->
    <link href="Content/Office.css" rel="stylesheet" type="text/css" />
    <script src="https://appsforoffice.microsoft.com/lib/1/hosted/office.js"></script>

    <!-- Для включения автономной отладки с помощью локальной ссылки на скрипт Office.js используйте:                  -->
    <!--    <script src="Scripts/Office/MicrosoftAjax.js" type="text/javascript"></script>       -->
    <!--    <script src="Scripts/Office/1/office.js" type="text/javascript"></script>          -->


Обратите внимание, что функцию инициализации Office необходимо вызвать до любого 
взаимодействия кода JavaScript с API Office (один раз на каждой странице):

    Office.initialize = function (reason) {
        $(document).ready(function () {
            // Добавьте сюда логику инициализации.
        });
    };
