# Graphs

### Задание 1
Для решения всех задач курса необходимо создать класс (или иерархию классов - на усмотрение разработчика), содержащий:

1. Структуру для хранения списка смежности графа (не работать с графом через матрицы смежности, если в некоторых алгоритмах удобнее использовать список ребер - реализовать метод, создающий список рёбер на основе списка смежности);
2. Конструкторы (не менее 3-х):
- конструктор по умолчанию, создающий пустой граф [x]
- конструктор, заполняющий данные графа из файла(из матрицы смежности) [x]
- конструктор-копию (аккуратно, не все сразу делают именно копию) [ ]
- специфические конструкторы для удобства тестирования [ ]
3. Методы:

- добавляющие вершину, [x]
- добавляющие ребро (дугу), [x]
- удаляющие вершину (удалить все смежные ребра), [x]
- удаляющие ребро (дугу), [x]
- выводящие список смежности в файл (в том числе в пригодном для чтения конструктором формате). [ ]

Не выполняйте некорректные операции, сообщайте об ошибках.

4. Должны поддерживаться как ориентированные, так и неориентированные графы. Заранее предусмотрите возможность добавления меток и\или весов для дуг. Поддержка мультиграфа не требуется.
5. Добавьте минималистичный консольный интерфейс пользователя (не смешивая его с реализацией!), позволяющий добавлять и удалять вершины и рёбра (дуги) и просматривать текущий список смежности графа.
6. Сгенерируйте не менее 4 входных файлов с разными типами графов (балансируйте на комбинации ориентированность-взвешенность) для тестирования класса в этом и последующих заданиях. Графы должны содержать не менее 7-10 вершин, в том числе петли и изолированные вершины.

Замечание:
В зависимости от выбранного способа хранения графа могут появиться дополнительные трудности при удалении-добавлении, например, необходимость переименования вершин, если граф хранится списком (например, vector C++, List C#). Этого можно избежать, если хранить в списке пару (имя вершины, список смежных вершин), или хранить в другой структуре (например, Dictionary C#, map в С++, при этом список смежности вершины может также храниться в виде словаря с ключами - смежными вершинами и значениями - весами соответствующих ребер). Идеально, если в качестве вершины реализуется обобщенный тип (generic), но достаточно использовать строковый тип или свой класс.
