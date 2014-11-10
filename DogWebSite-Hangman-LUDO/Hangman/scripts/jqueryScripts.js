$(document).ready(function () {
    var word = {};
    var wrongGuess = 0;
    var rightGuess = 0;
    var visited = [];

    $("li").click(function () {
        if (Object.keys(word).length === 0) {
            return;
        }

        var letter = $(this).html();

        if (visited.indexOf(letter) > -1) {
            alert("You have already selected this letter!");
            return;
        }

        visited.push(letter);
        $(this).css("background-color", "red");

        var indices = getMatchingIndices(letter, word.word);

        if (indices.length > 0) {
            for (var i = 0; i < indices.length; i++) {
                var number = indices[i] + 1;
                var selector = "#content div:nth-of-type(" + number + ") p";
                $(selector).css('visibility', 'visible');
            }

            rightGuess += indices.length;

            if (rightGuess == word.word.length) {
                alert("YOU WIN!");
                $("input[type=button]").show();
                window.location.reload();
            }
        }
        else {
            var number = wrongGuess + 1;
            var path = "url('../images/" + number + ".png')";
            $("#field").css("background-image", path);
            wrongGuess++;

            if (wrongGuess == 6) {
                alert("GAME OVER");
                $("input[type=button]").show();
                window.location.reload();
            }
        }
    });

    $("input[type=button]").click(function () {
        word = generateWord();
        $("div[id=content]").html(word.div.innerHTML);
        $(this).hide();
    });
});