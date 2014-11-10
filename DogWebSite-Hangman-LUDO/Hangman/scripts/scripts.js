function generateWord() {
    var words = [
        "apple",
        "building",
        "monument",
        "vegetable",
        "skydiving",
        "airplane",
        "jacket",
        "hunter",
        "policeman",
        "grandmother",
        "mathematics",
        "manufacturing",
        "discussion",
        "arrangement",
        "explanation",
        "university"
    ];

    var index = Math.floor(Math.random() * 15);
    var word = words[index];

    var length = word.length;

    var div = document.createElement("DIV");
    for (var i = 0; i < length; i++) {
        var divToAppend = document.createElement("DIV");

        var p = document.createElement("P");
        p.textContent = word[i];
        divToAppend.appendChild(p);
        div.appendChild(divToAppend);
    }

    var obj = {};
    obj.div = div;
    obj.word = word;
    return obj;
}

function getMatchingIndices(letter, word) {
    var indices = [];

    for (var i = 0; i < word.length; i++) {
        if (letter == word[i]) {
            indices.push(i);
        }
    }

    return indices;
}
