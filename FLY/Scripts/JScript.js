function RepeatString(str, ex, num) {
    var new_ex = "";
    if (typeof (str) == "string") {
        new_str = str;
    }
    else {
        new_str = str.toString();
    }
    for (var i = 0; i < (num - new_str.length); i++) {
        new_ex = new_ex + ex;
    }
    new_ex = new_ex + new_str;
    return new_ex;
}
function GridViewCheckboxSelectAll(CheckBoxName) {
    var checkBoxs = div.getElementsByTagName(CheckBoxName);
    for (var i = 0; i < CheckBoxs.length; i++)
    { CheckBoxs.checked = true; }
}
function GridViewCheckboxReselectAll(CheckBox) {
    var CheckBoxString = new String(document.getElementById(CheckBox).value)
    var CheckBoxArray = CheckBoxString.split("|")
    for (var i = 0; i < CheckBoxArray.length; i++) {
        var box = document.getElementById(CheckBoxArray[i]);
        if (box.checked == true)
        { box.checked = false; }
        else
        { box.checked = true; }
    }
}
function GridViewCheckboxDeselectAll(CheckBox) {
    var CheckBoxString = new String(document.getElementById(CheckBox).value)
    var CheckBoxArray = CheckBoxString.split("|")
    for (var i = 0; i < CheckBoxArray.length; i++)
    { document.getElementById(CheckBoxArray[i]).checked = false; }
}
function CheckboxSelectAll(CheckBox) {
    var CheckBoxString = new String(document.getElementById(CheckBox).value)
    var CheckBoxArray = CheckBoxString.split("|")
    for (var i = 0; i <= CheckBoxArray.length; i++)
    { document.getElementById(Array_CheckBox[i]).checked = true; }
}
function CheckboxReselectAll(CheckBox) {
    var CheckBoxString = new String(document.getElementById(CheckBox).value)
    var CheckBoxArray = CheckBoxString.split("|")
    for (var i = 0; i <= Array_CheckBox.length - 1; i++) {
        var box = document.getElementById(Array_CheckBox[i]);
        if (box.checked == true)
        { box.checked = false; }
        else
        { box.checked = true; }
    }
}
function CheckboxDeselectAll(CheckBox) {
    var CheckBoxString = new String(document.getElementById(CheckBox).value)
    var CheckBoxArray = CheckBoxString.split("|")
    for (var i = 0; i <= Array_CheckBox.length - 1; i++)
    { document.getElementById(Array_CheckBox[i]).checked = false; }
}