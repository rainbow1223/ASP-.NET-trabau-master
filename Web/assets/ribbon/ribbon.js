var fontFamily = ['Segoe UI', 'Times New Roman', 'Arial', 'Times New Roman', 'Tahoma', 'Helvetica'];
var fontSize = ['1pt', '2pt', '3pt', '4pt', '5pt'];
var tabs = [{
    id: 'home',
    text: 'Home',
    groups: [{
        text: 'Clipboard',
        alignType: ej.Ribbon.AlignType.Rows,
        content: [{
            groups: [{
                id: 'copy',
                text: 'Copy',
                toolTip: 'Copy',
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'e-icon flaticon-copy'
                }
            }, {
                id: 'paste',
                text: 'Paste',
                toolTip: 'Paste',
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'e-icon flaticon-paste'
                }
            }, {
                id: 'cut',
                text: 'Cut',
                toolTip: 'Cut',
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'e-icon flaticon-scissors'
                }
            }, {
                id: 'delete',
                text: 'Delete',
                toolTip: 'Delete',
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'e-icon flaticon-delete'
                }
            }, {
                id: 'export',
                text: 'Export',
                toolTip: 'Export',
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'e-icon flaticon-share'
                }
            }]
        }],
        enableGroupExpander: true,
    }, {
        text: 'Undo Action',
        alignType: ej.Ribbon.AlignType.Columns,
        width: 80,
        content: [
            {
                groups: [{
                    id: 'undo',
                    text: 'Undo Last Action',
                    toolTip: 'Undo',
                    buttonSettings: {
                        contentType: ej.ContentType.TextAndImage,
                        imagePosition: ej.ImagePosition.ImageTop,
                        prefixIcon: 'e-icon flaticon-back-arrow'
                    }
                }]
            }]
    }, {
        text: 'Format',
        alignType: ej.Ribbon.AlignType.Rows,
        content: [{
            groups: [{
                id: 'fontFamily',
                toolTip: 'Font',

                dropdownSettings: {
                    dataSource: fontFamily,
                    text: 'Segoe UI',
                    width: 100,
                }
            }, {
                id: 'fontSize',
                toolTip: 'FontSize',
                dropdownSettings: {
                    dataSource: fontSize,
                    text: '1pt',
                    width: 60
                }
            }],
            defaults: {
                type: ej.Ribbon.type.dropDownList,
                height: 28
            }
        }, {
            groups: [{
                id: 'bold',
                toolTip: 'Bold',
                type: ej.Ribbon.type.toggleButton,
                toggleButtonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    defaultText: 'Bold',
                    activeText: 'Bold',
                    defaultPrefixIcon: 'e-icon flaticon-bold',
                    activePrefixIcon: 'e-icon flaticon-bold'
                }
            }, {
                id: 'italics',
                toolTip: 'Italics',
                type: ej.Ribbon.type.toggleButton,
                toggleButtonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    defaultText: 'Italics',
                    activeText: 'Italics',
                    defaultPrefixIcon: 'e-icon flaticon-italic',
                    activePrefixIcon: 'e-icon flaticon-italic'
                }
            }, {
                id: 'underLine',
                toolTip: 'Underline',
                type: ej.Ribbon.type.toggleButton,
                toggleButtonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    defaultText: 'Underline',
                    activeText: 'Underline',
                    defaultPrefixIcon: 'e-icon flaticon-underlined-text',
                    activePrefixIcon: 'e-icon flaticon-underlined-text'
                }
            }, {
                id: 'strikeThrough',
                toolTip: 'Strike Through',
                type: ej.Ribbon.type.toggleButton,
                toggleButtonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    defaultText: 'Strike Thorugh',
                    activeText: 'Strike Through',
                    defaultPrefixIcon: 'e-icon flaticon-strikethrough',
                    activePrefixIcon: 'e-icon flaticon-strikethrough'
                }
            }, {
                id: 'leftalign',
                toolTip: 'Left align',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'e-icon flaticon-left-align'
                }
            }, {
                id: 'centeralign',
                toolTip: 'Center align',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'e-icon flaticon-center-align'
                }
            }, {
                id: 'rightalign',
                toolTip: 'Right align',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'e-icon flaticon-right-align'
                }
            }],

        }]
    },

    {
        text: 'Style',
        alignType: ej.Ribbon.AlignType.Rows,
        content: [{
            groups: [{
                id: 'fill',
                text: 'Fill',
                toolTip: 'Fill',
                width: 40,
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'flaticon-bucket-large'
                }
            }, {
                id: 'line',
                text: 'Line',
                toolTip: 'Line',
                width: 40,
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'flaticon-pencil-drawing-a-line-large'
                }
            }, {
                id: 'text',
                text: 'Text',
                toolTip: 'Text',
                width: 40,
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'flaticon-text-box-large'
                }
            }, {
                id: 'start',
                text: 'Start',
                toolTip: 'Start',
                width: 40,
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'flaticon-double-arrow-large'
                }
            }, {
                id: 'end',
                text: 'End',
                toolTip: 'End',
                width: 40,
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'flaticon-double-arrow-large'
                }
            }]
        }]
    }, {
        text: 'Annotation',
        alignType: ej.Ribbon.AlignType.Columns,
        content: [{
            groups: [{
                id: 'pointer',
                text: 'Pointer',
                toolTip: 'Pointer',
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'flaticon-cursor'
                }
            }]
        }, {
            groups: [{
                id: 'textBox',
                text: 'Text',
                toolTip: 'Text Box',
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'flaticon-capital-a'
                }
            }]
        }, {
            groups: [{
                id: 'line',
                text: 'Line',
                toolTip: 'Line',
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'flaticon-diagonal-line'
                }
            }]
        }, {
            groups: [{
                id: 'rectangle',
                text: 'Rectangle',
                toolTip: 'Rectangle',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'flaticon-rectangular-shape-outline'
                }
            }, {
                id: 'arc',
                text: 'Arc',
                toolTip: 'Arc',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'flaticon-curved-line'
                }
            }]
        }, {
            groups: [{
                id: 'ellipse',
                text: 'Ellipse',
                toolTip: 'Ellipse',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'flaticon-ellipse-outline-shape-variant'
                }
            }, {
                id: 'polyline',
                text: 'Polyline',
                toolTip: 'Polyline',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'flaticon-business-ascendant-graphic-line'
                }
            }]
        }]
    }, {
        text: 'Communication Link',
        alignType: ej.Ribbon.AlignType.Columns,
        content: [{
            groups: [{
                id: 'straight',
                text: 'Straight',
                toolTip: 'Straight',
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'flaticon-line'
                }
            }]
        }, {
            groups: [{
                id: 'sidetoside',
                toolTip: 'Side to Side Communication Link',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'flaticon-line'
                }
            }, {
                id: 'toptobottomside',
                toolTip: 'Top to Bottom Side to Side',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'flaticon-line'
                }
            }]
        }, {
            groups: [{
                id: 'lefttoptobottom',
                toolTip: 'Left Top to Bottom Communication Link',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'flaticon-line'
                }
            }, {
                id: 'singledirection',
                toolTip: 'Single Direction Communication Link',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'flaticon-right-arrow'
                }
            }]
        }, {
            groups: [{
                id: 'toptobottomright',
                toolTip: 'Top to Bottom right Communication Link',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'flaticon-line'
                }
            }, {
                id: 'doubledirection',
                toolTip: 'Double Direction Communication Link',
                buttonSettings: {
                    contentType: ej.ContentType.ImageOnly,
                    prefixIcon: 'flaticon-double-arrows'
                }
            }]
        }]
    }, {
        text: 'Node Item',
        content: [{
            groups: [{
                id: 'node',
                text: 'Node',
                toolTip: 'Node',
                buttonSettings: {
                    contentType: ej.ContentType.TextAndImage,
                    imagePosition: ej.ImagePosition.ImageTop,
                    prefixIcon: 'flaticon-website-1'
                }
            }]
        }]
    }]
}
];

$(function () {
    $('#Ribbon').ejRibbon({
        expandPinSettings: {
            toolTip: 'Collapse the Ribbon'
        },
        // application tab
        allowResizing: true,
        applicationTab: {
            type: ej.Ribbon.applicationTabType.menu,
            menuItemID: 'ribbon',
            menuSettings: {
                openOnClick: true
            }
        },

        tabs: tabs,
        groupExpand: function (args) {
            alert('Expanded')
        },
    });
});