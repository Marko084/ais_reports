    String.prototype.toCamelCase = function () {
        return this
            .replace(/\s(.)/g, function ($1) { return $1.toUpperCase(); })
            .replace(/\s/g, '')
            .replace(/^(.)/, function ($1) { return $1.toUpperCase(); });
    };

    String.prototype.toProperCase = function () {
        return this
            .replace(/\s(.)/g, function ($1) { return $1.toUpperCase(); })
            .replace(/\s/g, '')
            .replace(/^(.)/, function ($1) { return $1.toUpperCase(); });
    };

    String.prototype.toTitleCase = function() {
        var words = this.toProperCase();
        var results ="";

        for (var i=0; i < words.length; i++) {
            var letter = words.charAt(i);

            if (i == 0) {
                results = results + letter;
            }
            else if (letter==letter.toUpperCase() && words.charAt(i-1) != words.charAt(i-1).toUpperCase())
            {
                results = results + " " + letter;
            }
            else {
                results = results + letter;
            }
        }

        if (results == "CSCNo") {
            results = "CSC No";
        }

        return results;
    };