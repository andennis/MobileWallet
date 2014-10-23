//On load window
jQuery(window).load(function () {
    var fileInput = document.getElementById('logoImageInput'),
        stripFileInput = document.getElementById('stripImageInput'),
        thumbnailFileInput = document.getElementById('thumbnailImageInput'),
        backgroundFileInput = document.getElementById('backgroundImageInput');
    fileInput.onchange = HandleChanges;
    stripFileInput.onchange = HandleChanges;
    thumbnailFileInput.onchange = HandleChanges;
    backgroundFileInput.onchange = HandleChanges;
    fileInput.value = '';
    stripFileInput.value = '';

    ChangeWidthHeaderTextFieldPass(1);
    ChangeWidthAuxiliaryTextFieldPass(1);
    ChangeWidthAuxiliaryTextFieldPass(2);
    ChangWidthFlexContainerHeaderPass();
});

//Replace all SVG images with inline SVG
jQuery('img.transitIconPass').each(function () {
    var $img = jQuery(this);
    var imgID = $img.attr('id');
    var imgClass = $img.attr('class');
    var imgURL = $img.attr('src');
    jQuery.get(imgURL, function (data) {
        // Get the SVG tag, ignore the rest
        var $svg = jQuery(data).find('svg');

        // Add replaced image's ID to the new SVG
        if (typeof imgID !== 'undefined') {
            $svg = $svg.attr('id', imgID);
        }
        // Add replaced image's classes to the new SVG
        if (typeof imgClass !== 'undefined') {
            $svg = $svg.attr('class', imgClass);
        }

        // Replace image with new SVG
        $img.replaceWith($svg);

    }, 'xml');

});

//Images inputs
function handleFileSelect(evt) {
    var files = evt.target.files,
    className = evt.target.id.replace('logo', 'spanLogo').replace('strip', 'spanStrip').replace('thumbnail', 'spanThumbnail').replace('background', 'spanBackground').replace('Input', 'Pass'),
    divId = evt.target.id.replace('logo', 'divLogo').replace('strip', 'divStrip').replace('thumbnail', 'divThumbnail').replace('background', 'divBackground').replace('Input', 'Pass'),
    span = document.createElement('span'),
    reader = new FileReader();

    // Only process image files.
    if (!files[0].type.match('image.*')) {
        return;
    }
    reader.onload = (function (theFile) {
        return function (e) {

            // Render thumbnail.
            span = document.createElement('span');
            span.innerHTML = ['<img class="' + className + '" src="', e.target.result, '" title="', escape(theFile.name), '" />'].join('');
            document.getElementById(divId).innerHTML = "";
            document.getElementById(divId).insertBefore(span, null);
        };
    })(files);

    // Read in the image file as a data URL.
    reader.readAsDataURL(files[0]);
    ChangeLeftPropertyLogoText();
    if (divId === 'divBackgroundImagePass') {
        BlurBackgroundImage();
    }
}
document.getElementById('logoImageInput').addEventListener('change', handleFileSelect, false);
document.getElementById('stripImageInput').addEventListener('change', handleFileSelect, false);
document.getElementById('thumbnailImageInput').addEventListener('change', handleFileSelect, false);
document.getElementById('backgroundImageInput').addEventListener('change', handleFileSelect, false);

function HandleChanges(evt) {
    var imgNameId = evt.target.id.replace('ImageInput', 'Name');
    var file = document.getElementById(evt.target.id).value;
    var reWin = /.*\\(.*)/;
    var fileTitle = file.replace(reWin, "$1"); //выдираем название файла
    var reUnix = /.*\/(.*)/;
    fileTitle = fileTitle.replace(reUnix, "$1"); //выдираем название файла
    document.getElementById(imgNameId).innerHTML = fileTitle;
    RepositionRemoveIcon();
    //var RegExExt = /.*\.(.*)/;
    //var ext = fileTitle.replace(RegExExt, "$1"); //и его расширение
}

//Blur background image
function BlurBackgroundImage() {
    stackBlurImage('backgroundImagePass', 'backgroundCanvas', 70, false);
}

//Remove image on pass
jQuery('.removeImage').click(function (evt) {
    var itemId = evt.target.id.replace('remove', '');
    console.log(itemId);
    document.getElementById('div' + itemId + 'ImagePass').innerHTML = '';
    itemId = itemId.toLowerCase();
    document.getElementById(itemId + 'Name').innerHTML = 'Файл не выбран';
    RepositionRemoveIcon();
});

//Hide “Image not found” icon when src source image is not found
$("img").error(function () {
    $(this).hide();
});

//Blur background image after loading
$("#backgroundImagePass").load(function () {
    BlurBackgroundImage();
});

//Reposition remove icon
function RepositionRemoveIcon() {
    var max = Math.max(document.getElementById('logoName').offsetWidth, document.getElementById('stripName')
    .offsetWidth, document.getElementById('thumbnailName').offsetWidth, document.getElementById('backgroundName').offsetWidth);
    document.getElementById('removeLogo').style.marginLeft = max + 203 + 'px';
    document.getElementById('removeStrip').style.marginLeft = max + 203 + 'px';
    document.getElementById('removeThumbnail').style.marginLeft = max + 203 + 'px';
    document.getElementById('removeBackground').style.marginLeft = max + 203 + 'px';
}

//Flippy effect
var j = jQuery.noConflict();
jQuery("#MainTab4").click(function () {
    jQuery('.frontTabPassBlock').css('display', 'none');
    if (!jQuery("#MainTab4").hasClass("active")) {
        j("#divPassBody").flippy({
            color_target: "#ffffff",
            content: j("#divbackPass"),
            direction: "LEFT",
            duration: "200"
        });
        j("#MainTab4").removeClass("flippy");
        return true;
    }

});

jQuery("#MainTab1, #MainTab2, #MainTab3, #MainTab5, #MainTab6, #MainTab7").click(function () {
    if (jQuery(this).attr('id') === 'MainTab3') {
        jQuery('.frontTabPassBlock').css('display', 'block');
    } else {
        jQuery('.frontTabPassBlock').css('display', 'none');
    }
    if (jQuery("#MainTab4").hasClass("active")) {
        j("#divPassBody").flippyReverse({
            //content: j("#" + (this).id),
            content: j("#divFrontPass"),
            direction: "RIGHT",
            duration: "350"
        });
    }
    if (jQuery('.passTypeImg.selected').attr('data-pass') === 'eventTicket') {
        setTimeout(function () { BlurBackgroundImage(); }, 150);
    }
});

//Main content tabs
jQuery('#topPanel p.tabMain').click(function () {
    var clickId = jQuery(this).attr('id');
    if (clickId !== jQuery('#topPanel p.active').attr('id')) {
        jQuery('#topPanel p.active').removeClass('active');
        jQuery(this).addClass('active');
        jQuery('#rightPanel div').removeClass('active');
        jQuery('#content' + clickId).addClass('active');
    }
});

//Front content tabs
jQuery('.linkTabFrontContent').click(function () {
    var clickId = jQuery(this).attr('id');
    if (clickId !== jQuery('.linkTabFrontContent.active').attr('id')) {
        jQuery('.linkTabFrontContent.active').removeClass('active');
        jQuery(this).addClass('active');
        jQuery('#wrapper div').removeClass('active');
        jQuery('#con_' + clickId).addClass('active');
    }
});
jQuery('#MainTab3').click(function () {
    jQuery('.linkTabFrontContent.active').removeClass('active');
    jQuery('#wrapper div').removeClass('active');
    jQuery('#con_tab0').addClass('active');
});

//Change type of barcode
jQuery('#divBarcodeFormat input').click(function () {
    var clickId = jQuery(this).attr('id');
    if (clickId !== jQuery('#divBarcodeFormat input.active').attr('id')) {
        jQuery('#divBarcodeFormat input.active').removeClass('active');
        jQuery(this).addClass('active');
        jQuery('#divBarcodePass img').removeClass('active');
        jQuery('#' + clickId + 'Pass').addClass('active');
    }
});

//Change text color
function ChangeLabelTextColor(color) {
    var labelElements = document.getElementsByClassName("labelText");
    for (var i = 0, labelElLength = labelElements.length; i < labelElLength; i++) {
        labelElements[i].style.color = color;
    }
    var transitIcons = document.getElementsByClassName("transitIconPass");
    for (var j = 0, transIconsLength = transitIcons.length; j < transIconsLength; j++) {
        transitIcons[j].style.fill = color;
    }
}

function ChangeValueTextColor(color) {
    var labelElements = document.getElementsByClassName("valueText");
    for (var i = 0, labelElLength = labelElements.length; i < labelElLength; i++) {
        labelElements[i].style.color = color;
    }
}

//Add field to back content
function AddFieldBackContent() {
    var lastChild = jQuery("#mainCollapsePanelBackContent").children().last(),
    lastChildId = parseInt(lastChild.attr('id').toString().slice(-2).replace('t', ''), 10),
    content = '',
    aHref = '',
    tempId = '',
    tempName = '',
    propCheckedFixedLabel = '',
    propCheckedDynamicLabel = '',
    propCheckedFixedValue = '',
    propCheckedDynamicValue = '',
    replacedHtml = '',
    //stringContent = '/Content' + lastChildID + '/g',
    stringContent = new RegExp('Content' + lastChildId, 'g'),
    stringBack = new RegExp('Back' + lastChildId, 'g'),
    stringInput = new RegExp('Input' + lastChildId, 'g'),
    stringField = new RegExp('Field' + lastChildId, 'g'),
    stringLabel = new RegExp('Label' + lastChildId, 'g'),
    stringValue = new RegExp('Value' + lastChildId, 'g');
    jQuery('<div />', {
        id: 'collapsePanelBackContent' + (lastChildId + 1)
    }).appendTo('#mainCollapsePanelBackContent');

    //Save property 'checked' radio buttons last field
    propCheckedFixedLabel = jQuery('#collapseContentLabelBack' + lastChildId + '1').prop('checked');
    propCheckedDynamicLabel = jQuery('#collapseContentLabelBack' + lastChildId + '2').prop('checked');
    propCheckedFixedValue = jQuery('#collapseContentValueBack' + lastChildId + '1').prop('checked');
    propCheckedDynamicValue = jQuery('#collapseContentValueBack' + lastChildId + '2').prop('checked');

    //Copy content last field to new field
    content = jQuery('#collapsePanelBackContent' + lastChildId).html();
    jQuery('#collapsePanelBackContent' + (lastChildId + 1)).html(content);

    //Change attributes in new field
    replacedHtml = jQuery('#collapsePanelBackContent' + (lastChildId + 1)).html().replace(stringContent, 'Content' + (lastChildId + 1)).replace(stringBack, 'Back' + (lastChildId + 1))
    .replace(stringInput, 'Input' + (lastChildId + 1)).replace(stringField, 'Field' + (lastChildId + 1))
    .replace(stringLabel, 'Label' + (lastChildId + 1)).replace(stringValue, 'Value' + (lastChildId + 1));
    jQuery('#collapsePanelBackContent' + (lastChildId + 1)).html(replacedHtml);

    jQuery('#collapseContentLabelBack' + lastChildId + '1').prop('checked', propCheckedFixedLabel);
    jQuery('#collapseContentLabelBack' + lastChildId + '2').prop('checked', propCheckedDynamicLabel);
    jQuery('#collapseContentValueBack' + lastChildId + '1').prop('checked', propCheckedFixedValue);
    jQuery('#collapseContentValueBack' + lastChildId + '2').prop('checked', propCheckedDynamicValue);

    DisplayNoneRadioButtonPrompt('collapseContentLabelBack' + (lastChildId + 1) + '1');
    DisplayNoneRadioButtonPrompt('collapseContentValueBack' + (lastChildId + 1) + '1');
    AddFieldBackContentPass(lastChildId + 1);
    jQuery("[data-toggle='tooltip']").tooltip();
}

//Add field to back content pass
function AddFieldBackContentPass(lastChildId) {
    if (jQuery('#mainCollapsePanelBackContent').children().size() === 1 && jQuery('#mainCollapsePanelBackContent').attr('class') === 'displayNone') {
        jQuery('#collapsePanelBackContent' + tempId).removeClass('displayNone');
        jQuery('#divBackFieldContentPass' + tempId).removeClass('displayNone');
    } else {
        jQuery('<div />', {
            id: 'divBackFieldContentPass' + lastChildId,
            class: 'divBackFieldContentPass'
        }).appendTo('#divBackContentPass');
        jQuery('<div />', {
            id: 'backLabelPass' + lastChildId,
            class: 'labelFieldBackPass'
        }).appendTo('#divBackFieldContentPass' + lastChildId);
        jQuery('<div />', {
            id: 'backValuePass' + lastChildId,
            class: 'valueFieldBackPass'
        }).appendTo('#divBackFieldContentPass' + lastChildId);
    }
}
//backLabelTextInput1     backLabelPass1
//Bind back inputs to pass fields
function BindBackInputToFieldPass(id, value) {
    var imputId = id.replace('TextInput', 'Pass');
    document.getElementById(imputId).innerHTML = value;
}

////Bind back textarea to pass fields
//function BindBackTextareaToFieldPass(id, value) {
//    var imputId = id.slice(-2).replace('t', '');
//    document.getElementById('valueFieldBackPass' + imputId).innerHTML = value;
//}

//Remove field from back content
function RemoveFieldFromBackContent(id) {
    var tempId = id.slice(-2).replace('d', '');
    if (jQuery('#mainCollapsePanelBackContent').children().size() === 1) {
        jQuery('#collapsePanelBackContent' + tempId).addClass('displayNone');
        jQuery('#divBackFieldContentPass' + tempId).addClass('displayNone');
    } else {
        jQuery('#collapsePanelBackContent' + tempId).remove();
        jQuery('#divBackFieldContentPass' + tempId).remove();
    }
}

//Remove all field in back content pass
function RemoveAllFieldBackPass() {
    jQuery("#divBackContentPass").html("");
}

function AddFieldPassAgain() {
    var childs = jQuery("#mainCollapsePanelBackContent").children().toArray(),
    tempId = '';
    for (var i = 0; i < childs.length; i++) {
        tempId = childs[i].getAttribute('id').slice(-2).replace('t', '');
        jQuery('<div />', {
            id: 'divBackFieldContentPass' + tempId,
            class: 'divBackFieldContentPass'
        }).appendTo('#divBackContentPass');
        jQuery('<div />', {
            id: 'backLabelPass' + tempId,
            class: 'labelFieldBackPass',
            text: jQuery('#inputBackFieldContent' + tempId).val()
        }).appendTo('#divBackFieldContentPass' + tempId);
        jQuery('<div />', {
            id: 'backValuePass' + tempId,
            class: 'valueFieldBackPass',
            text: jQuery('#textareaBackFieldContent' + tempId).val()
        }).appendTo('#divBackFieldContentPass' + tempId);
    }
}

//Change width text fields on header pass area
function ChangeWidthHeaderTextFieldPass(someId) {
    var tempId = someId.toString().slice(-1),
    max = 0;
    document.getElementById('divHeaderLabelPass' + tempId).style.width = 'auto';
    document.getElementById('divHeaderValuePass' + tempId).style.width = 'auto';
    max = Math.max(document.getElementById('divHeaderLabelPass' + tempId).offsetWidth, document.getElementById('divHeaderValuePass' + tempId).offsetWidth);
    if (max > 0) {
        document.getElementById('divHeaderLabelPass' + tempId).style.width = max + 'px';
        document.getElementById('divHeaderValuePass' + tempId).style.width = max + 'px';
    }
    ChangWidthFlexContainerHeaderPass();
}



//Change width flex container on header text area
function ChangWidthFlexContainerHeaderPass() {
    var tempWidth = 0,
    maxWidth = 165;
    jQuery('#divLogoTextPass').css('width', 'auto');
    tempWidth = 270 - (jQuery('#divLogoImagePass').width() + jQuery('#divLogoTextPass').width()) - 14;
    if (tempWidth < maxWidth) {
        jQuery('.flexContainerHeaderFields').css('width', tempWidth + 'px');
        jQuery('.flexContainerHeaderFields').css('left', 105 + (maxWidth - tempWidth) + 'px');
    } else {
        jQuery('.flexContainerHeaderFields').css('width', maxWidth + 'px');
        jQuery('.flexContainerHeaderFields').css('left', 105 + 'px');
    }
}

//Change 'left' property logo text on pass after resize or delete logo image
window.onload = ChangeLeftPropertyLogoText();

jQuery('#divLogoImagePass').bind("DOMSubtreeModified", function () {
    ChangeLeftPropertyLogoText();
});

function ChangeLeftPropertyLogoText() {
    var logoImageWidth = jQuery('#divLogoImagePass').width();
    jQuery('#divLogoTextPass').css('left', (logoImageWidth + 8).toString() + 'px');
    ChangWidthFlexContainerHeaderPass();
}

//Change width text fields on primary pass area
function ChangeWidthPrimaryTextFieldPass(someId) {
    var tempId = someId.toString().slice(-1),
    max = 0;
    document.getElementById('divPrimaryLabelPass' + tempId).style.width = 'auto';
    document.getElementById('divPrimaryValuePass' + tempId).style.width = 'auto';
    max = Math.max(document.getElementById('divPrimaryLabelPass' + tempId).offsetWidth, document.getElementById('divPrimaryValuePass' + tempId).offsetWidth);
    if (max > 0) {
        document.getElementById('divPrimaryLabelPass' + tempId).style.width = max + 'px';
        document.getElementById('divPrimaryValuePass' + tempId).style.width = max + 'px';
    }
}

//Change width text fields on auxiliary pass area
function ChangeWidthAuxiliaryTextFieldPass(someId) {
    var tempId = someId.toString().slice(-1),
    max = 0;
    document.getElementById('divAuxiliaryLabelPass' + tempId).style.width = 'auto';
    document.getElementById('divAuxiliaryValuePass' + tempId).style.width = 'auto';
    max = Math.max(document.getElementById('divAuxiliaryLabelPass' + tempId).offsetWidth, document.getElementById('divAuxiliaryValuePass' + tempId).offsetWidth);
    if (max > 0) {
        document.getElementById('divAuxiliaryLabelPass' + tempId).style.width = max + 'px';
        document.getElementById('divAuxiliaryValuePass' + tempId).style.width = max + 'px';
    }
}

//Change width text fields on secondary pass area
function ChangeWidthSecondaryTextFieldPass(someId) {
    var tempId = someId.toString().slice(-1),
    max = 0;
    document.getElementById('divSecondaryLabelPass' + tempId).style.width = 'auto';
    document.getElementById('divSecondaryValuePass' + tempId).style.width = 'auto';
    max = Math.max(document.getElementById('divSecondaryLabelPass' + tempId).offsetWidth, document.getElementById('divSecondaryValuePass' + tempId).offsetWidth);
    if (max > 0) {
        document.getElementById('divSecondaryLabelPass' + tempId).style.width = max + 'px';
        document.getElementById('divSecondaryValuePass' + tempId).style.width = max + 'px';
    }
}

//Check and uncheck fieds on front pass
function CheckAndUncheckFieldFront(id) {
    var tempId = id.replace('checkbox', 'div'),
    number = tempId.slice(-1),
    tempIdLabel = '',
    tempIdValue = '';
    tempIdLabel = tempId.replace(number, 'LabelPass' + number);
    tempIdValue = tempId.replace(number, 'ValuePass' + number);
    if (jQuery('#' + id).prop('checked')) {
        jQuery('#' + tempIdLabel).css('display', 'block');
        jQuery('#' + tempIdValue).css('display', 'block');
    } else {
        jQuery('#' + tempIdLabel).css('display', 'none');
        jQuery('#' + tempIdValue).css('display', 'none');
    }
}

//Check and uncheck fieds on back pass
function CheckAndUncheckFieldBack(id) {
    var number = id.slice(-2).replace('k', ''),
    tempId = 'divBackFieldContentPass' + number;
    if (jQuery('#' + id).prop('checked')) {
        jQuery('#' + tempId).css('display', 'block');
    } else {
        jQuery('#' + tempId).css('display', 'none');
    }
}

//Check checkbox field after input text in front content
function CheckCheckboxAfterInput(id) {
    var tempId = id.toString().replace('LabelTextInput', '').replace('ValueTextInput', '').replace('LabelCollapseInput', '')
    .replace('input', '').replace('textarea', '').replace('FieldContent', '');
    tempId = tempId.toString().charAt(0).toUpperCase() + tempId.slice(1);
    tempId = 'checkbox' + tempId;
    if (!jQuery('#' + tempId).prop('checked')) {
        jQuery('#' + tempId).attr('checked', 'checked');
        CheckAndUncheckFieldFront(tempId);
        CheckAndUncheckFieldBack(tempId);
    }
}

//Display block dynamic radio button prompt
function DisplayRadioButtonPrompt(id) {
    var idPrompt = id.toString().slice(0, -1);
    document.getElementById(idPrompt + 'Prompt').style.display = 'block';
}

//Display none dynamic radio button prompt
function DisplayNoneRadioButtonPrompt(id) {
    var idPrompt = id.toString().slice(0, -1);
    document.getElementById(idPrompt + 'Prompt').style.display = 'none';
}


//Change placeholder text input
jQuery('input.radioInputFixOrDinamicLabel').change(function () {
    var tempId = jQuery(this).attr('id').slice(0, -1).replace('Radios', 'TextInput'),
    number = jQuery(this).attr('id').slice(-1);

    if (number === '1') {
        jQuery('#' + tempId).attr('placeholder', 'Обязательный');
    } else if (number === '2') {
        jQuery('#' + tempId).attr('placeholder', 'Необязательный');
    }
});

jQuery('input.radioInputFixOrDinamicValue').change(function () {
    var tempId = jQuery(this).attr('id').slice(0, -1).replace('Radios', 'TextInput'),
    number = jQuery(this).attr('id').slice(-1);
    if (number === '1') {
        jQuery('#' + tempId).attr('placeholder', 'Обязательное');
    } else if (number === '2') {
        jQuery('#' + tempId).attr('placeholder', 'Необязательное');
    }
});

//Display block serial number input on pass settings tab
function DisplayBlockSerialNumberInput() {
    jQuery('#divSerialNumberSettingsTab').css('display', 'block');
}

//Display none serial number input on pass settings tab
function DisplayNoneSerialNumberInput() {
    jQuery('#divSerialNumberSettingsTab').css('display', 'none');
}

//Display none checkbox on collapse area
function DisplayNoneCollapseCheckbox(name) {
    var tempId = name.toString().replace('optionsRadios', 'divCheckbox');
    jQuery('#' + tempId).css('display', 'none');
}

//Display block checkbox on collapse area
function DisplayBlockCollapseCheckbox(name) {
    var tempId = name.toString().replace('optionsRadios', 'divCheckbox');
    jQuery('#' + tempId).css('display', 'block');
}

//Display the desired select list on change data type select in front content tab
function DisplayBlockDesiredSelect(thisId) {
    var thisValue = jQuery('#' + thisId + ' option:selected').val(),
    numberFormatId = thisId.replace('select', 'divNumberFormat'),
    currencyId = thisId.replace('select', 'divCurrency'),
    dateFormatId = thisId.replace('select', 'divDateFormat');
    jQuery('#' + numberFormatId).css('display', 'none');
    jQuery('#' + currencyId).css('display', 'none');
    jQuery('#' + dateFormatId).css('display', 'none');
    switch (thisValue) {
        case 'number':
            jQuery('#' + numberFormatId).css('display', 'block');
            break;
        case 'currency':
            jQuery('#' + currencyId).css('display', 'block');
            break;
        case 'date':
            jQuery('#' + dateFormatId).css('display', 'block');
            break;
        case 'dateTime':
            jQuery('#' + dateFormatId).css('display', 'block');
            break;
    }
};

//Add address field on lock screen
var GLOBAL_COUNT_ADDRESS_FIELD = 1;
function AddAddressField() {
    GLOBAL_COUNT_ADDRESS_FIELD++;
    if (GLOBAL_COUNT_ADDRESS_FIELD <= 10) {
        var lastChild = jQuery("#mainCollapsePanelAddressContent").children().last(),
        lastChildId = parseInt(lastChild.attr('id').toString().slice(-2).replace('t', ''), 10),
        content = '',
        replacedHtml = '',
        stringContent = new RegExp('Content' + lastChildId, 'g'),
        stringAddress = new RegExp('Address' + lastChildId, 'g'),
        stringInput = new RegExp('Input' + lastChildId, 'g'),
        stringMap = new RegExp('Map' + lastChildId, 'g'),
        stringLock = new RegExp('Lock' + lastChildId, 'g'),
        stringButton = new RegExp('Button' + lastChildId, 'g');
        jQuery('<div />', {
            id: 'collapsePanelAddressContent' + (lastChildId + 1)
        }).appendTo('#mainCollapsePanelAddressContent');

        //Copy content last field to new field
        content = jQuery('#collapsePanelAddressContent' + lastChildId).html();
        jQuery('#collapsePanelAddressContent' + (lastChildId + 1)).html(content);

        //Change attributes in new field
        replacedHtml = jQuery('#collapsePanelAddressContent' + (lastChildId + 1)).html().replace(stringContent, 'Content' + (lastChildId + 1))
        .replace(stringAddress, 'Address' + (lastChildId + 1)).replace(stringInput, 'Input' + (lastChildId + 1)).replace(stringMap, 'Map' + (lastChildId + 1))
        .replace(stringLock, 'Lock' + (lastChildId + 1)).replace(stringButton, 'Button' + (lastChildId + 1));
        jQuery('#collapsePanelAddressContent' + (lastChildId + 1)).html(replacedHtml);
    }
    jQuery("[data-toggle='tooltip']").tooltip();
};

//Add Language input text row on languages tab
function AddLanguageTextRow(thisId) {
    var tempId = parseInt(thisId.slice(-3).replace('n', '').replace('o', ''), 10),
    lastChild = jQuery('#divLanguageTextInputs' + tempId).children().last(),
    lastChildId = parseInt(lastChild.attr('id').toString().slice(-2).replace('w', ''), 10),
    content = '',
    replacedHtml = '',
    stringOriginal = new RegExp('Original' + lastChildId, 'g'),
    stringLocal = new RegExp('Local' + lastChildId, 'g');
    jQuery('<div />', {
        id: 'divTextInputsLanguage' + tempId + 'Row' + (lastChildId + 1)
    }).appendTo('#divLanguageTextInputs' + tempId);

    //Copy content last field to new field
    content = jQuery('#divTextInputsLanguage' + tempId + 'Row' + lastChildId).html();
    jQuery('#divTextInputsLanguage' + tempId + 'Row' + (lastChildId + 1)).html(content);

    //Change attributes in new field
    replacedHtml = jQuery('#divTextInputsLanguage' + tempId + 'Row' + (lastChildId + 1)).html().replace(stringOriginal, 'Original' + (lastChildId + 1))
    .replace(stringLocal, 'Local' + (lastChildId + 1));
    jQuery('#divTextInputsLanguage' + tempId + 'Row' + (lastChildId + 1)).html(replacedHtml);
}

//Add language field on language tab
function AddLanguageField() {
    var lastChild = jQuery('#mainCollapsePanelLanguageContent').children().last(),
    lastChildId = parseInt(lastChild.attr('id').toString().slice(-2).replace('t', ''), 10),
    content = '',
    replacedHtml = '',
    stringContent = new RegExp('Content' + lastChildId, 'g'),
    stringLanguage = new RegExp('Language' + lastChildId, 'g'),
    stringInputs = new RegExp('Inputs' + lastChildId, 'g'),
    stringButton = new RegExp('Button' + lastChildId, 'g');
    jQuery('<div />', {
        id: 'collapsePanelLanguageContent' + (lastChildId + 1)
    }).appendTo('#mainCollapsePanelLanguageContent');

    //Copy last field content to new field
    content = jQuery('#collapsePanelLanguageContent' + lastChildId).html();
    jQuery('#collapsePanelLanguageContent' + (lastChildId + 1)).html(content);

    //Change attributes in new field
    replacedHtml = jQuery('#collapsePanelLanguageContent' + lastChildId).html().replace(stringContent, 'Content' + (lastChildId + 1))
    .replace(stringLanguage, 'Language' + (lastChildId + 1)).replace(stringInputs, 'Inputs' + (lastChildId + 1)).replace(stringButton, 'Button' + (lastChildId + 1));
    jQuery('#collapsePanelLanguageContent' + (lastChildId + 1)).html(replacedHtml);
}

//Display block or none distribution quantity input on distribution tab
function DisplayDistQuantityInput(tempValue) {
    switch (tempValue) {
        case '0':
            jQuery('#divDistQuantityInput').css('display', 'none');
            break;
        case '1':
            jQuery('#divDistQuantityInput').css('display', 'block');
            break;
    }
}

//Display block or none distribution date restriction input on distribution tab
function DisplayDistDateRestrInput(tempValue) {
    switch (tempValue) {
        case '0':
            jQuery('#divDistDateRestrInput').css('display', 'none');
            break;
        case '1':
            jQuery('#divDistDateRestrInput').css('display', 'block');
            break;
    }
}

//Display block or none distribution issue password input on distribution tab
function DisplayDistIssuePasswdInput(tempValue) {
    switch (tempValue) {
        case '0':
            jQuery('#divDistIssuePasswdInput').css('display', 'none');
            break;
        case '1':
            jQuery('#divDistIssuePasswdInput').css('display', 'block');
            break;
        case '2':
            jQuery('#divDistIssuePasswdInput').css('display', 'none');
            break;
    }
}

//Display block or none distribution update password input on distribution tab
function DisplayDistUpdatePasswdInput(tempValue) {
    switch (tempValue) {
        case '0':
            jQuery('#divDistUpdatePasswdInput').css('display', 'none');
            break;
        case '1':
            jQuery('#divDistUpdatePasswdInput').css('display', 'block');
            break;
    }
}

//Display block or none barcode message textarea on front content tab
function DisplayBarcodeMessTextarea(tempValue) {
    switch (tempValue) {
        case 'encodeThePassSerialNumber':
            jQuery('#divBarcodeMessageTextarea').css('display', 'none');
            break;
        case 'encodeThePassUniqueId':
            jQuery('#divBarcodeMessageTextarea').css('display', 'none');
            break;
        case 'encodeTheUrlToUpdateThePass':
            jQuery('#divBarcodeMessageTextarea').css('display', 'none');
            break;
        case 'provideWhenPassIsCreated':
            jQuery('#divBarcodeMessageTextarea').css('display', 'none');
            break;
        case 'encodeTheSameMessageOnEachPass':
            jQuery('#divBarcodeMessageTextarea').css('display', 'block');
            break;
    }
}

//Display block or none barcode alternative text input on front content tab
function DisplayBarcodeAltText(tempValue) {
    switch (tempValue) {
        case '0':
            jQuery('#divAlternativeTextInput').css('display', 'none');
            break;
        case 'displayThePassSerialNumber':
            jQuery('#divAlternativeTextInput').css('display', 'none');
            break;
        case 'displayThePassUniqueId':
            jQuery('#divAlternativeTextInput').css('display', 'none');
            break;
        case 'provideWhenPassIsCreated':
            jQuery('#divAlternativeTextInput').css('display', 'none');
            break;
        case 'displayTheSameMessageOnEachPass':
            jQuery('#divAlternativeTextInput').css('display', 'block');
            break;
        case 'doNotDisplayAnyAlternativeText':
            jQuery('#divAlternativeTextInput').css('display', 'none');
            break;
    }
}

//Display desired transit icon on pass
function displayTransitIcon(tempValue) {
    jQuery('.transitIconPass').css('display', 'none');
    jQuery('#' + tempValue + 'IconPass').css('display', 'block');
}


//Google map on lock screen tab
var geocoder;
var MAP = [];
var markersArray = [];
MARKER_ARR = [];
var LATITUDE = 53.9;
var LONGITUDE = 27.56666670000004;
var TEMP_LAT;
var TEMP_LNG;
MAP.length = 10;
MARKER_ARR.length = 10;

//Initialization, create google map
function initialize(tempId) {
    var idNumber = tempId.slice(-2).replace('s', '');
    if (!MAP.hasOwnProperty(idNumber - 1)) {
        tempId = tempId.replace('pButtonAddress', 'gMap');
        geocoder = new google.maps.Geocoder();
        var latlng = new google.maps.LatLng(LATITUDE, LONGITUDE);
        var mapOptions = {
            zoom: 11,
            center: latlng
        };
        var map = new google.maps.Map(document.getElementById(tempId), mapOptions);
        MAP[idNumber - 1] = map;
        google.maps.event.addListener(map, 'click', function (event) {
            placeMarker(event.latLng, tempId);
        });
    } else {
        if (!(MARKER_ARR[idNumber - 1] == null)) {
            markersArray.push(MARKER_ARR[idNumber - 1]);
            var marker = new google.maps.Marker({
                map: MAP[idNumber - 1],
                position: MARKER_ARR[idNumber - 1].position
            });
            markersArray.push(marker);
        }
    }

}

//Call find address on map function after click button 'Найти'
function FindButtonCodeAddress(tempId) {
    var varId = tempId.replace('ButtonFind', 'Input');
    codeAddress(varId);
}

//Call find address on map function after click button 'Найти широту и долготу'
function LatLngButtonCodeAddress(tempId) {
    var varId = tempId.replace('pButtonAddress', 'addressLabelCollapseInput');
    var idNumber = tempId.slice(-2).replace('s', '');
    jQuery('#addressInputLock' + idNumber).val(jQuery('#addressLabelCollapseInput' + idNumber).val());
    if (jQuery('#addressLabelCollapseInput' + idNumber).val() != '') {
        codeAddress(varId);
    }
}

//Find address on map
function codeAddress(tempId) {
    var idNumber = parseInt(tempId.slice(-2).replace('k', '').replace('t', ''), 10);
    var address = document.getElementById(tempId).value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            deleteMapMarkers();
            MAP[idNumber - 1].setCenter(results[0].geometry.location);
            MAP[idNumber - 1].setZoom(17);
            var marker = new google.maps.Marker({
                map: MAP[idNumber - 1],
                position: results[0].geometry.location
            });
            markersArray.push(marker);
            MARKER_ARR[idNumber - 1] = marker;
            tempId = tempId.replace('addressInputLock', 'latLngMapContent').replace('addressLabelCollapseInput', 'latLngMapContent');
            document.getElementById(tempId).innerHTML = marker.position.lat() + '/' + marker.position.lng();
            TEMP_LAT = marker.position.lat();
            TEMP_LNG = marker.position.lng();
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

//Resize map
function ResizeMap(tempId) {
    var idNumber = parseInt(tempId.slice(-2).replace('s', ''), 10);
    google.maps.event.trigger(MAP[idNumber - 1], 'resize');
    LatLngButtonCodeAddress(tempId);
}

//Place marker on map
function placeMarker(location, tempId) {
    var idNumber = parseInt(tempId.slice(-2).replace('p', ''), 10);
    deleteMapMarkers();
    var marker = new google.maps.Marker({
        position: location,
        map: MAP[idNumber - 1]
    });
    markersArray.push(marker);
    MARKER_ARR[idNumber - 1] = marker;
    tempId = tempId.replace('gMap', 'latLngMapContent');
    document.getElementById(tempId).innerHTML = location.lat() + '/' + location.lng();
    TEMP_LAT = location.lat();
    TEMP_LNG = location.lng();
    //var infowindow = new google.maps.InfoWindow({
    //    content: 'Latitude: ' + location.lat() + 'Longitude: ' + location.lng()
    //});
    //infowindow.open(map, marker);
}

//Delete all markers on map
function deleteMapMarkers() {
    for (var i = 0; i < markersArray.length; i++) {
        markersArray[i].setMap(null);
    }
    markersArray.length = 0;
}

//Apply latitude and longitude button on lock screen tab
function LatLngApply(tempId) {
    var latInputId,
    lngInputId,
    addressInputId,
    addressInputMapId;
    latInputId = tempId.replace('latLngApplyButton', 'latitudeInput');
    lngInputId = tempId.replace('latLngApplyButton', 'longitudeInput');
    addressInputId = tempId.replace('latLngApplyButton', 'addressLabelCollapseInput');
    addressInputMapId = tempId.replace('latLngApplyButton', 'addressInputLock');
    document.getElementById(latInputId).value = TEMP_LAT;
    document.getElementById(lngInputId).value = TEMP_LNG;
    document.getElementById(addressInputId).value = document.getElementById(addressInputMapId).value;
    DisNoneDarkAreaAndMap();
}

//Display none dark area and map on lock screen
function DisNoneDarkAreaAndMap() {
    jQuery('.gMapLockScreen').css('display', 'none');
    jQuery('#darkAreaLockScreen').css('display', 'none');
}

//Display block dark area and map on lock screen
function DisBlockDarkAreaAndMap(tempId) {
    var idNumber = parseInt(tempId.slice(-2).replace('s', ''), 10);
    jQuery('#divGMap' + idNumber).css('display', 'block');
    jQuery('#darkAreaLockScreen').css('display', 'block');
}

//Tooltip bootstrap
jQuery(function () {
    jQuery("[data-toggle='tooltip']").tooltip();
});

jQuery(function () {
    jQuery("[data-toggle='collapse']").collapse();
});

//Show date time pickers on distribution tab
function ShowDateTimePicker(thisId) {
    jQuery('#' + thisId).appendDtpicker({
        'locale': 'ru',
        'todayButton': false,
        'dateFormat': 'DD/MM/YY hh:mm',
        'firstDayOfWeek': 1,
        'closeOnSelected': true
    });
    jQuery('#' + thisId).handleDtpicker('show');
}

//Change barcode type function
jQuery('#divBarcodeFormat input').click(function () {
    switch (jQuery(this).attr('id')) {
        case 'aztecCode':
            jQuery('#tab5').css('top', '378px').css('height', '126px');
            if (jQuery('.passTypeImg.selected').attr('data-pass') === 'generic') {
                jQuery('#auxiliaryAreaPass').css('display', 'none');
                jQuery('#tab3').css('display', 'none');
            }
            break;
        case 'qrCode':
            jQuery('#tab5').css('top', '378px').css('height', '126px');
            if (jQuery('.passTypeImg.selected').attr('data-pass') === 'generic') {
                jQuery('#auxiliaryAreaPass').css('display', 'none');
                jQuery('#tab3').css('display', 'none');
            }
            break;
        case 'pdf417Code':
            jQuery('#tab5').css('top', '397px').css('height', '102px');
            if (jQuery('.passTypeImg.selected').attr('data-pass') === 'generic') {
                jQuery('#auxiliaryAreaPass').css('display', 'block');
                jQuery('#tab3').css('display', 'block');
            }
            break;
    }
    if (jQuery('.passTypeImg.selected').attr('data-pass') === 'eventTicket' && jQuery('#eventTicketType input:checked').attr('value') === 'option2') {
        ChangesDependingPassType('eventTicket');
    };
});

//Check and uncheck pass type radio button (images)
jQuery('.passTypeImg').click(function () {
    var attrValue;
    jQuery('.passTypeImg, selected').removeClass('selected');
    jQuery(this).addClass('selected');
    attrValue = jQuery(this).attr('data-pass');
    jQuery('#divPassType input[checked = "checked"]').removeAttr('checked');
    jQuery('#divPassType input[value = "' + attrValue + '"]').attr('checked', 'checked');
    ChangesDependingPassType(attrValue);
});

//Change event ticket type radio button
jQuery('input[name=optionsEventTicketType]:radio').change(function () {
    ChangesDependingPassType('eventTicket');
});

//Change the settings depending on the pass type
function ChangesDependingPassType(passType) {
    switch (passType) {
        case 'boardingPass':

            //Images
            jQuery('#divStripImage').css('display', 'none');
            jQuery('#divStripImagePass').css('display', 'none');
            jQuery('#divBackgroundImage').css('display', 'none');
            jQuery('#backgroundCanvas').css('display', 'none');
            jQuery('#divThumbnailImage').css('display', 'none');
            jQuery('#divThumbnailImagePass').css('display', 'none');

            //Auxiliary
            jQuery('#divAuxiliaryLabelPass4').css('display', 'block');
            jQuery('#divAuxiliaryLabelPass5').css('display', 'block');
            jQuery('#divAuxiliaryValuePass4').css('display', 'block');
            jQuery('#divAuxiliaryValuePass5').css('display', 'block');
            jQuery('#flexContainerAuxiliaryLabels').css('top', '105px');
            jQuery('#flexContainerAuxiliaryValues').css('top', '121px');
            jQuery('#collapsePanelAuxiliaryContent4').css('display', 'block');
            jQuery('#collapsePanelAuxiliaryContent5').css('display', 'block');
            jQuery('#auxiliaryAreaPass').css('display', 'block');
            jQuery('#tab3').css('display', 'block').css('height', '40px').css('top', '267px');

            //Primary
            jQuery('#collapsePanelPrimaryContent2').css('display', 'block');
            jQuery('#divPrimaryValuePass2').css('display', 'block');
            jQuery('#divPrimaryLabelPass2').css('display', 'block').removeClass('valueText').removeClass('labelText').addClass('labelText');
            jQuery('#divPrimaryLabelPass2').css('color', jQuery('#labelColorPicker').val());
            jQuery('#divPrimaryValuePass1').css('font-size', '25px');
            jQuery('#divPrimaryLabelPass1').css('font-size', '10px').css('display', 'block').removeClass('valueText').removeClass('labelText').addClass('labelText');
            jQuery('#divPrimaryLabelPass1').css('color', jQuery('#labelColorPicker').val());
            jQuery('#divPrimaryLabelText1').css('display', 'block');
            jQuery('#flexContainerPrimaryValues').css('top', '66px').css('height', '40px').css('width', '257px');
            jQuery('#flexContainerPrimaryLabels').css('top', '55px').css('height', '13px').css('width', '257px');
            jQuery('#tab2').css('height', '40px').css('top', '222px');
            jQuery('#transitIconsBlock').css('display', 'block');

            //Secondary
            jQuery('#secondaryAreaPass').css('display', 'block');
            jQuery('#flexContainerSecondaryLabels').css('top', '152px');
            jQuery('#flexContainerSecondaryValues').css('top', '165px');
            jQuery('.flexContainerSecondaryFields').css('width', '257px');
            jQuery('#collapsePanelSecondaryContent3').css('display', 'block');
            jQuery('#collapsePanelSecondaryContent4').css('display', 'block');
            jQuery('#tab4').css('display', 'block').css('top', '313px').css('height', '40px');

            //Others
            jQuery('#divGroupingIdentifier').css('display', 'block');
            jQuery('#divEventTicketTypeRadio').css('display', 'none');
            break;
        case 'coupon':

            //Images
            jQuery('#divStripImage').css('display', 'block');
            jQuery('#divStripImagePass').css('display', 'block');
            jQuery('#divBackgroundImage').css('display', 'none');
            jQuery('#backgroundCanvas').css('display', 'none');
            jQuery('#divThumbnailImage').css('display', 'none');
            jQuery('#divThumbnailImagePass').css('display', 'none');
            jQuery('.divStripImagePass').css('max-height', '106px');

            //Auxiliary
            jQuery('#divAuxiliaryLabelPass4').css('display', 'none');
            jQuery('#divAuxiliaryLabelPass5').css('display', 'none');
            jQuery('#divAuxiliaryValuePass4').css('display', 'none');
            jQuery('#divAuxiliaryValuePass5').css('display', 'none');
            jQuery('#flexContainerAuxiliaryLabels').css('top', '168px');
            jQuery('#flexContainerAuxiliaryValues').css('top', '181px');
            jQuery('#collapsePanelAuxiliaryContent4').css('display', 'none');
            jQuery('#collapsePanelAuxiliaryContent5').css('display', 'none');
            jQuery('#auxiliaryAreaPass').css('display', 'block');
            jQuery('#tab3').css('display', 'block').css('height', '43px').css('top', '329px');

            //Primary
            jQuery('#collapsePanelPrimaryContent2').css('display', 'none');
            jQuery('#divPrimaryValuePass2').css('display', 'none');
            jQuery('#divPrimaryLabelPass2').css('display', 'none').removeClass('valueText').removeClass('labelText').addClass('valueText');
            jQuery('#divPrimaryValuePass1').css('font-size', '58px');
            jQuery('#divPrimaryLabelPass1').css('font-size', '14px').css('display', 'block').removeClass('valueText').removeClass('labelText').addClass('valueText');
            jQuery('#divPrimaryLabelPass1').css('color', jQuery('#textColorPicker').val());
            jQuery('#divPrimaryLabelText1').css('display', 'block');
            jQuery('#flexContainerPrimaryValues').css('top', '48px').css('height', '70px').css('width', '257px');
            jQuery('#flexContainerPrimaryLabels').css('top', '122px').css('height', '36px').css('width', '257px');
            jQuery('#tab2').css('height', '94px').css('top', '222px');
            jQuery('#transitIconsBlock').css('display', 'none');

            //Secondary
            jQuery('#secondaryAreaPass').css('display', 'none');
            jQuery('#tab4').css('display', 'none').css('top', '329px').css('height', '43px');

            //Others
            jQuery('#divGroupingIdentifier').css('display', 'none');
            jQuery('#divEventTicketTypeRadio').css('display', 'none');
            break;
        case 'storeCard':

            //Images
            jQuery('#divStripImage').css('display', 'block');
            jQuery('#divStripImagePass').css('display', 'block');
            jQuery('#divBackgroundImage').css('display', 'none');
            jQuery('#backgroundCanvas').css('display', 'none');
            jQuery('#divThumbnailImage').css('display', 'none');
            jQuery('#divThumbnailImagePass').css('display', 'none');
            jQuery('.divStripImagePass').css('max-height', '106px');

            //Auxiliary
            jQuery('#divAuxiliaryLabelPass4').css('display', 'none');
            jQuery('#divAuxiliaryLabelPass5').css('display', 'none');
            jQuery('#divAuxiliaryValuePass4').css('display', 'none');
            jQuery('#divAuxiliaryValuePass5').css('display', 'none');
            jQuery('#flexContainerAuxiliaryLabels').css('top', '168px');
            jQuery('#flexContainerAuxiliaryValues').css('top', '181px');
            jQuery('#collapsePanelAuxiliaryContent4').css('display', 'none');
            jQuery('#collapsePanelAuxiliaryContent5').css('display', 'none');
            jQuery('#auxiliaryAreaPass').css('display', 'block');
            jQuery('#tab3').css('display', 'block').css('height', '43px').css('top', '329px');

            //Primary
            jQuery('#collapsePanelPrimaryContent2').css('display', 'none');
            jQuery('#divPrimaryValuePass2').css('display', 'none');
            jQuery('#divPrimaryLabelPass2').css('display', 'none').removeClass('valueText').removeClass('labelText').addClass('valueText');
            jQuery('#divPrimaryValuePass1').css('font-size', '58px');
            jQuery('#divPrimaryLabelPass1').css('font-size', '14px').css('display', 'block').removeClass('valueText').removeClass('labelText').addClass('valueText');
            jQuery('#divPrimaryLabelPass1').css('color', jQuery('#textColorPicker').val());
            jQuery('#divPrimaryLabelText1').css('display', 'block');
            jQuery('#flexContainerPrimaryValues').css('top', '48px').css('height', '70px').css('width', '257px');
            jQuery('#flexContainerPrimaryLabels').css('top', '122px').css('height', '36px').css('width', '257px');
            jQuery('#tab2').css('height', '94px').css('top', '222px');
            jQuery('#transitIconsBlock').css('display', 'none');

            //Secondary
            jQuery('#secondaryAreaPass').css('display', 'none');
            jQuery('#tab4').css('display', 'none').css('top', '329px').css('height', '43px');

            //Others
            jQuery('#divGroupingIdentifier').css('display', 'none');
            jQuery('#divEventTicketTypeRadio').css('display', 'none');
            break;
        case 'generic':

            //Images
            jQuery('#divStripImage').css('display', 'none');
            jQuery('#divStripImagePass').css('display', 'none');
            jQuery('#divBackgroundImage').css('display', 'none');
            jQuery('#backgroundCanvas').css('display', 'none');
            jQuery('#divThumbnailImage').css('display', 'block');
            jQuery('#divThumbnailImagePass').css('display', 'block');
            jQuery('#thumbnailImagePass').removeClass('eventTicket').addClass('generic');

            //Auxiliary
            jQuery('#divAuxiliaryLabelPass4').css('display', 'block');
            jQuery('#divAuxiliaryLabelPass5').css('display', 'none');
            jQuery('#divAuxiliaryValuePass4').css('display', 'block');
            jQuery('#divAuxiliaryValuePass5').css('display', 'none');
            jQuery('#flexContainerAuxiliaryLabels').css('top', '195px');
            jQuery('#flexContainerAuxiliaryValues').css('top', '210px');
            jQuery('#collapsePanelAuxiliaryContent4').css('display', 'block');
            jQuery('#collapsePanelAuxiliaryContent5').css('display', 'none');
            jQuery('#tab3').css('height', '36px').css('top', '357px');
            if (!(jQuery('#divBarcodeFormat input.active').attr('id') === 'pdf417Code')) {
                jQuery('#auxiliaryAreaPass').css('display', 'none');
                jQuery('#tab3').css('display', 'none');
            }

            //Primary
            jQuery('#collapsePanelPrimaryContent2').css('display', 'none');
            jQuery('#divPrimaryValuePass2').css('display', 'none');
            jQuery('#divPrimaryLabelPass2').css('display', 'none').removeClass('valueText').removeClass('labelText').addClass('labelText');
            jQuery('#divPrimaryValuePass1').css('font-size', '16px');
            jQuery('#divPrimaryLabelPass1').css('font-size', '10px').css('display', 'block').removeClass('valueText').removeClass('labelText').addClass('labelText');
            jQuery('#divPrimaryLabelPass1').css('color', jQuery('#labelColorPicker').val());
            jQuery('#divPrimaryLabelText1').css('display', 'block');
            jQuery('#flexContainerPrimaryValues').css('top', '77px').css('height', '40px').css('width', '168px');
            jQuery('#flexContainerPrimaryLabels').css('top', '66px').css('height', '13px').css('width', '168px');
            jQuery('#tab2').css('height', '49px').css('top', '227px');
            jQuery('#transitIconsBlock').css('display', 'none');

            //Secondary
            jQuery('#secondaryAreaPass').css('display', 'block');
            jQuery('#flexContainerSecondaryLabels').css('top', '152px');
            jQuery('#flexContainerSecondaryValues').css('top', '165px');
            jQuery('.flexContainerSecondaryFields').css('width', '257px');
            jQuery('#collapsePanelSecondaryContent3').css('display', 'none');
            jQuery('#collapsePanelSecondaryContent4').css('display', 'none');
            jQuery('#tab4').css('display', 'block').css('top', '313px').css('height', '40px');

            //Others
            jQuery('#divGroupingIdentifier').css('display', 'none');
            jQuery('#divEventTicketTypeRadio').css('display', 'none');
            break;
        case 'eventTicket':
            if (jQuery('#eventTicketType input:checked').attr('value') === 'option1') {

                //Images
                jQuery('#divStripImage').css('display', 'none');
                jQuery('#divStripImagePass').css('display', 'none');
                jQuery('#divBackgroundImage').css('display', 'block');
                jQuery('#backgroundCanvas').css('display', 'block');
                jQuery('#divThumbnailImage').css('display', 'block');
                jQuery('#divThumbnailImagePass').css('display', 'block');
                jQuery('#thumbnailImagePass').removeClass('generic').addClass('eventTicket');

                //Auxiliary
                jQuery('#divAuxiliaryLabelPass4').css('display', 'block');
                jQuery('#divAuxiliaryLabelPass5').css('display', 'none');
                jQuery('#divAuxiliaryValuePass4').css('display', 'block');
                jQuery('#divAuxiliaryValuePass5').css('display', 'none');
                jQuery('#flexContainerAuxiliaryLabels').css('top', '144px');
                jQuery('#flexContainerAuxiliaryValues').css('top', '160px');
                jQuery('#collapsePanelAuxiliaryContent4').css('display', 'block');
                jQuery('#collapsePanelAuxiliaryContent5').css('display', 'none');
                jQuery('#auxiliaryAreaPass').css('display', 'block');
                jQuery('#tab3').css('display', 'block').css('height', '40px').css('top', '306px');

                //Primary
                jQuery('#collapsePanelPrimaryContent2').css('display', 'none');
                jQuery('#divPrimaryValuePass2').css('display', 'none');
                jQuery('#divPrimaryLabelPass2').css('display', 'none').removeClass('valueText').removeClass('labelText').addClass('labelText');
                jQuery('#divPrimaryValuePass1').css('font-size', '16px');
                jQuery('#divPrimaryLabelPass1').css('font-size', '10px').css('display', 'block').removeClass('valueText').removeClass('labelText').addClass('labelText');
                jQuery('#divPrimaryLabelPass1').css('color', jQuery('#labelColorPicker').val());
                jQuery('#divPrimaryLabelText1').css('display', 'block');
                jQuery('#flexContainerPrimaryValues').css('top', '66px').css('height', '25px').css('width', '168px');
                jQuery('#flexContainerPrimaryLabels').css('top', '55px').css('height', '13px').css('width', '168px');
                jQuery('#tab2').css('height', '34px').css('top', '222px');
                jQuery('#transitIconsBlock').css('display', 'none');

                //Secondary
                jQuery('#secondaryAreaPass').css('display', 'block');
                jQuery('#flexContainerSecondaryLabels').css('top', '100px');
                jQuery('#flexContainerSecondaryValues').css('top', '114px');
                jQuery('.flexContainerSecondaryFields').css('width', '199px');
                jQuery('#collapsePanelSecondaryContent3').css('display', 'none');
                jQuery('#collapsePanelSecondaryContent4').css('display', 'none');
                jQuery('#tab4').css('display', 'block').css('top', '264px').css('height', '34px');

                //Others
                jQuery('#divGroupingIdentifier').css('display', 'block');
                jQuery('#divEventTicketTypeRadio').css('display', 'block');
            } else if (jQuery('#eventTicketType input:checked').attr('value') === 'option2') {

                //Images
                jQuery('#divStripImage').css('display', 'block');
                jQuery('#divStripImagePass').css('display', 'block');
                jQuery('#divBackgroundImage').css('display', 'none');
                jQuery('#backgroundCanvas').css('display', 'none');
                jQuery('#divThumbnailImage').css('display', 'none');
                jQuery('#divThumbnailImagePass').css('display', 'none');
                jQuery('.divStripImagePass').css('max-height', '74px');

                //Auxiliary
                jQuery('#divAuxiliaryLabelPass4').css('display', 'block');
                jQuery('#divAuxiliaryLabelPass5').css('display', 'none');
                jQuery('#divAuxiliaryValuePass4').css('display', 'block');
                jQuery('#divAuxiliaryValuePass5').css('display', 'none');
                jQuery('#collapsePanelAuxiliaryContent4').css('display', 'block');
                jQuery('#collapsePanelAuxiliaryContent5').css('display', 'none');
                jQuery('#auxiliaryAreaPass').css('display', 'block');
                if (jQuery('#divBarcodeFormat input.active').attr('id') == 'pdf417Code') {
                    jQuery('#flexContainerAuxiliaryLabels').css('top', '184px');
                    jQuery('#flexContainerAuxiliaryValues').css('top', '200px');
                    jQuery('#tab3').css('display', 'block').css('height', '40px').css('top', '345px');
                } else {
                    jQuery('#flexContainerAuxiliaryLabels').css('top', '176px');
                    jQuery('#flexContainerAuxiliaryValues').css('top', '192px');
                    jQuery('#tab3').css('display', 'block').css('height', '35px').css('top', '339px');
                };

                //Primary
                jQuery('#collapsePanelPrimaryContent2').css('display', 'none');
                jQuery('#divPrimaryValuePass2').css('display', 'none');
                jQuery('#divPrimaryLabelPass2').css('display', 'none');
                jQuery('#divPrimaryValuePass1').css('font-size', '57px');
                jQuery('#divPrimaryLabelPass1').css('display', 'none');
                jQuery('#divPrimaryLabelPass1').css('color', jQuery('#labelColorPicker').val());
                jQuery('#divPrimaryLabelText1').css('display', 'none');
                jQuery('#flexContainerPrimaryValues').css('top', '49px').css('height', '65px').css('width', '257px');
                jQuery('#tab2').css('height', '64px').css('top', '222px');
                jQuery('#transitIconsBlock').css('display', 'none');

                //Secondary
                jQuery('#secondaryAreaPass').css('display', 'block');
                jQuery('.flexContainerSecondaryFields').css('width', '199px');
                jQuery('#collapsePanelSecondaryContent3').css('display', 'block');
                jQuery('#collapsePanelSecondaryContent4').css('display', 'none');
                if (jQuery('#divBarcodeFormat input.active').attr('id') == 'pdf417Code') {
                    jQuery('#flexContainerSecondaryLabels').css('top', '135px');
                    jQuery('#flexContainerSecondaryValues').css('top', '149px');
                    jQuery('#tab4').css('display', 'block').css('top', '297px').css('height', '40px');
                } else {
                    jQuery('#flexContainerSecondaryLabels').css('top', '130px');
                    jQuery('#flexContainerSecondaryValues').css('top', '144px');
                    jQuery('#tab4').css('display', 'block').css('top', '294px').css('height', '35px');
                }

                //Others
                jQuery('#divGroupingIdentifier').css('display', 'block');
                jQuery('#divEventTicketTypeRadio').css('display', 'block');
            }
            break;
    }
}

////Hex to rgb conversion
//function HexToRgb(hex) {
//    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
//    return 'rgb(' + parseInt(result[1], 16) + ', ' + parseInt(result[2], 16) + ', ' + parseInt(result[3], 16) + ')';
//}

//JSON serialization
function PostJsonData() {
    var jsonObj = {
        LocationDetails: {
            Locations: []
        },
        DistributionDetails: {},
        BarcodeDetails: {},
        FieldDetails: {
            AuxiliaryFields: [],
            BackFields: [],
            HeaderFields: [],
            PrimaryFields: [],
            SecondaryFields: []
        }
    },
    fieldsName = [
    { 'fieldName': 'auxiliary', 'FieldName': 'Auxiliary', 'fieldCount': 5, 'arrName': jsonObj.FieldDetails.AuxiliaryFields },
    { 'fieldName': 'back', 'FieldName': 'Back', 'fieldCount': jQuery('#mainCollapsePanelBackContent').children().length, 'arrName': jsonObj.FieldDetails.BackFields },
    { 'fieldName': 'header', 'FieldName': 'Header', 'fieldCount': 3, 'arrName': jsonObj.FieldDetails.HeaderFields },
    { 'fieldName': 'primary', 'FieldName': 'Primary', 'fieldCount': 2, 'arrName': jsonObj.FieldDetails.PrimaryFields },
    { 'fieldName': 'secondary', 'FieldName': 'Secondary', 'fieldCount': 4, 'arrName': jsonObj.FieldDetails.SecondaryFields }
    ];

    jsonObj.passProjectId = jQuery('#passProjectId').val();

    //Standard Keys
    jsonObj.OrganizationName = jQuery('#organizationNameInput').val();
    jsonObj.TemplateName = jQuery('#templateNameInput').val();
    //jsonObj.TemplateDescription = ?????????????????????????
    jsonObj.PassType = jQuery('#divPassType input[checked = "checked"]').val();
    jsonObj.PassDescription = jQuery('#passDescriptionTextarea').val();
    jsonObj.PassSerialNumberType = jQuery('#serialNumber input:checked').val();
    //if (jQuery('#serialNumber input:checked').attr('value') == 'option3') {
    //    jsonObj.serialNumber = jQuery('#serialNumberInput').val();
    //}
    jsonObj.PassCertificate = jQuery('#passCertificSelect option:selected').val();
    //jsonObj.TeamIdentifier = ??????????????????????????

    //Visual Appearance Keys
    jsonObj.BackgroundColor = jQuery('#backColorPicker').val();
    jsonObj.LabelTextColor = jQuery('#labelColorPicker').val();
    jsonObj.ValueTextColor = jQuery('#textColorPicker').val();
    //jsonObj.SuppressStripShine = ????????????????????????????(only before IOS7)

    //IOS 7
    //WARNING! Optional for event tickets and boarding passes; otherwise not allowed
    jsonObj.GroupingIdentifier = jQuery('#groupingIdentifierInput').val();
    jsonObj.PassTimezone = jQuery('#passTz').val();//invalid value
    jsonObj.LogoText = jQuery('#logoTextInput').val();

    //Integration Details
    //jsonObj.IntegrationDetails = ?????????????????????

    //Location Details
    jsonObj.LocationDetails.MaxDistance = jQuery('#maxDistanceInput').val();
    for (var i = 0; i < jQuery('#mainCollapsePanelAddressContent').children().length; i++) {
        jsonObj.LocationDetails.Locations.push({
            'Altitude': jQuery('#altitudeInput' + (i + 1)).val().replace('.', ','),
            'Latitude': jQuery('#latitudeInput' + (i + 1)).val().replace('.', ','),
            'Longitude': jQuery('#longitudeInput' + (i + 1)).val().replace('.', ','),
            'RelevantText': jQuery('#notificationInputAddress' + (i + 1)).val()
        });
    };//no deserialize

    //Beacon Details
    //jsonObj.BeaconDetails = ?????????????????????

    //Distribution Details
    jsonObj.DistributionDetails.PassLinkType = jQuery('#distTypeSelect option:selected').val();
    //jsonObj.DistributionDetails.LimitPassPerUser = ???????????
    jsonObj.DistributionDetails.AllPassesAsExpired = jQuery('#voidedCheckbox').is(':checked');
    jsonObj.DistributionDetails.ExpirationDate = jQuery('#autoExpireInput').val();
    if (jQuery('#distQuantitySelect :selected').val() === 1) {
        jsonObj.DistributionDetails.QuantityRestriction = jQuery('#distQuantityInput').val();
    }
    if (jQuery('#distDateRestrSelect :selected').val() === 1) {
        jsonObj.DistributionDetails.DateRestriction = jQuery('#distDateRestrInput').val();
    }
    if (jQuery('#distPasswdSelect :selected').val() === 1) {
        jsonObj.DistributionDetails.PasswordToIssue = jQuery('#distIssuePasswdInput').val();
    }
    if (jQuery('#distPasswdUpdSelect :selected').val() === 1) {
        jsonObj.DistributionDetails.PasswordToUpdate = jQuery('#distUpdatePasswdInput').val();
    }

    //Barcode Details
    jsonObj.BarcodeDetails.BarcodeType = jQuery('#divBarcodeFormat input.active').attr('id');//не хватает одного пункта
    jsonObj.BarcodeDetails.EncodedMessage = jQuery('#barcodeMessageSelect option:selected').val();
    jsonObj.BarcodeDetails.TextToEncode = jQuery('#barcodeMessageTextarea').val();
    jsonObj.BarcodeDetails.AlternativeText = jQuery('#barcodeAltTextSelect option:selected').val();
    jsonObj.BarcodeDetails.TextToDisplay = jQuery('#alternativeTextInput').val();
    jsonObj.BarcodeDetails.EncodingFormat = jQuery('#encodingFormatSelect option:selected').val();

    //Field Details
    for (var j = 0; j < fieldsName.length; j++) {
        for (var a = 0; a < fieldsName[j].fieldCount; a++) {
            fieldsName[j].arrName.push({
                'IsMarkedField': jQuery('#checkbox' + fieldsName[j].FieldName + (a + 1)).is(':checked'),

                //Standard Field Dictionary Keys
                //'attributedValue': ????????,
                'ChangeMessage': jQuery('#notificationInput' + fieldsName[j].FieldName + (a + 1)).val(),
                //'dataDetectorTypes': чекбоксы
                'Key': jQuery('#' + fieldsName[j].fieldName + 'LabelCollapseInput' + (a + 1)).val(),
                'Label': jQuery('#' + fieldsName[j].fieldName + 'LabelTextInput' + (a + 1)).val(),
                'IsDynamicLabel': jQuery('#' + fieldsName[j].fieldName + 'LabelRadios' + (a + 1) + '2').is(':checked'),
                'Value': jQuery('#' + fieldsName[j].fieldName + 'ValueTextInput' + (a + 1)).val(),
                'IsDynamicValue': jQuery('#' + fieldsName[j].fieldName + 'ValueRadios' + (a + 1) + '2').is(':checked'),
                //'textAlignment': ????????
                'Type': jQuery('#selectDataType' + fieldsName[j].FieldName + (a + 1) + ' option:selected').val(),

                //Number Style Keys
                'NumberStyle': jQuery('#selectNumbStyleType' + fieldsName[j].FieldName + (a + 1) + ' option:selected').val(),
                'CurrencyCode': jQuery('#currencyCodeSelect' + fieldsName[j].FieldName + (a + 1)).val(),

                //Date Style Keys
                'DateStyle': jQuery('#selectDateStyleType' + fieldsName[j].FieldName + (a + 1) + ' option:selected').val(),//without none value
                'IgnoresTimeZone': jQuery('#checkboxIgnoreData' + fieldsName[j].FieldName + (a + 1)).attr('checked') == 'checked',
                'IsRelative': jQuery('#checkboxRelativeData' + fieldsName[j].FieldName + (a + 1)).attr('checked') == 'checked'

                //'timeStyle': чекбокс??????
            });
        }
    }


    //var formData = new Object();
    //var files = $("#logoImageInput").get(0).files;
    //if (files.length > 0) {
    //    formData.Images = JSON.stringify(jsonObj);
    //}
    //formData.TemplateJson = JSON.stringify(jsonObj);

    //$.ajax({
    //    url: "/PassDesigner/UploadImages",
    //    type: 'POST',
    //    dataType: 'json',
    //    data: JSON.stringify(formData),
    //    contentType: 'application/json; charset=utf-8'
    //    });










    var data = new FormData();
    var logo = $("#logoImageInput").get(0).files;
    var strip = $("#stripImageInput").get(0).files;
    var background = $("#backgroundImageInput").get(0).files;
    var thumbnail = $("#thumbnailImageInput").get(0).files;

    data.append('jsonData', new Blob([JSON.stringify(jsonObj)], {
        type: "application/json; charset=utf-8"
    }));

    if (logo.length > 0) {
        data.append('logo', logo[0]);
    }
    if (strip.length > 0) {
        data.append('strip', strip[0]);
    }
    if (background.length > 0) {
        data.append('background', background[0]);
    }
    if (thumbnail.length > 0) {
        data.append('thumbnail', thumbnail[0]);
    }

    $.ajax({
        url: "/PassDesigner/Edit",
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        error: function (er) {
            alert(er);
        }
    });



    //jQuery.ajax({
    //    type: "POST",
    //    url: "/PassDesigner/Edit",
    //    data: JSON.stringify(jsonObj),
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (path) {
    //        var d = path;
    //        alert(d);
    //        console.log(path);
    //    }
    //});
    //alert(JSON.stringify(jsonObj));
}
