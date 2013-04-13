$(document).ready(function () {
    $.ajax({
        url: window.location.pathname + '/Home/RegexTree',
        data: { composedRegex: $("#codeTextArea").val() },
        type: 'POST',
        success: function (data) {
            //alert(data);
            var root = $("#treeRegex");
            var baseText = data.final;

            function addItem(e) {

                var nBaseText = "";
                var currentKey = e != null ? $(e.currentTarget).html() : null;

                var divParent = $("<div></div>");
                var items = [];

                var index = 0;

                for (var i in data.groups) {
                    var item = data.groups[i];
                    if (baseText.indexOf(item.Key) != -1) {
                        var spanItem = $("<span style='color:red'></span>");
                        spanItem.html(item.Key);
                        if (currentKey == item.Key) {
                            spanItem.html(item.Value);
                        }



                        var beforeSpan = $("<span></span>");
                        var str = baseText.substring(index, baseText.indexOf(item.Key));

                        beforeSpan.html(str);
                        if (str != null && str.length > 0) {
                            divParent.append(beforeSpan)
                            nBaseText += str;

                        }

                        index = baseText.indexOf(item.Key) + item.Key.length;

                        divParent.append(spanItem);
                        nBaseText += spanItem.html();


                        if (currentKey != item.Key) {
                            spanItem.bind("click", addItem);
                        }


                    }
                }

                var endSpan = $("<span></span>");
                var str2 = baseText.substring(index, baseText.length);

                endSpan.html(str2);
                if (str2 != null && str2.length > 0) {
                    divParent.append(endSpan);
                    nBaseText += str2;
                }

                root.append(divParent);
                baseText = nBaseText;

            }

            addItem();



        }
    });
});