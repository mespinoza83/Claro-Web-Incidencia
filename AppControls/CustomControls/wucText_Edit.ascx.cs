using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class Text_EditField : System.Web.UI.UserControl
{
    #region VARIABLES

    public enum ExpressionTypeEnum {Integer=1,Number, LetterUpperCase, LetterLowerCase, LetterAndNumber ,None,Email,Percent,BothCase,Parameter};
    public enum CustomExpressionTypeEnum {Custom,Number };

    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //revTextEdit.Enabled = false;
            //tbTextEdit_TextBoxWatermarkExtender.Enabled = false;
            //tbTextEdit_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
            if (tbTextEdit.TextMode == TextBoxMode.MultiLine)
            {
                tbTextEdit.Attributes["OnKeyPress"] = "return ValidateMaxLength(this," + tbTextEdit.MaxLength + ");";
                tbTextEdit.Attributes["OnKeyUp"] = "return StopMessage(this," + tbTextEdit.MaxLength + "); ";
                //tbTextEdit.Attributes["onfocus"] = "textAreaFocus(this," + tbTextEdit.MaxLength + ");";
            }
            tbTextEdit.Focus();
        }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 08/07/2011
    /// Descripcion: Establecer  tipo de dato predefinido que se puede introducir en el control TextBox
    /// </summary>
    public void SetDefinedValidator(ExpressionTypeEnum definedExpressionType)
    {
        revTextEdit.Enabled = false;
        tbTextEdit_FilteredTextBoxExtender.Enabled = true;
        tbTextEdit_TextBoxWatermarkExtender.Enabled = false;
        switch (definedExpressionType)
        {
            case ExpressionTypeEnum.Integer:
                tbTextEdit_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                break;
            case ExpressionTypeEnum.LetterUpperCase:
                tbTextEdit_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.UppercaseLetters;
                break;
            case ExpressionTypeEnum.LetterLowerCase:
                tbTextEdit_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.LowercaseLetters;
                break;
            case ExpressionTypeEnum.None:
                tbTextEdit_FilteredTextBoxExtender.Enabled = false;
                rfvTextEdit.Enabled = false;
                break;

            case ExpressionTypeEnum.Number:
                revTextEdit.Enabled = true;
                revTextEdit.ValidationExpression = "[0-9]+(\\.[0-9][0-9]?)?";
                tbTextEdit_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                tbTextEdit_FilteredTextBoxExtender.ValidChars = "0123456789.";
                break;
            case ExpressionTypeEnum.Email:
                tbTextEdit_TextBoxWatermarkExtender.Enabled = true;
                tbTextEdit_FilteredTextBoxExtender.Enabled = false;
                rfvTextEdit.Enabled = false;
                revTextEdit.Enabled = true;
                revTextEdit.ValidationExpression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                break;

            case ExpressionTypeEnum.LetterAndNumber:
                tbTextEdit_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                tbTextEdit_FilteredTextBoxExtender.ValidChars = "qwertyuiopasdfghjklñzxcvbnmQWERTYUIOPASDFGHJKLÑZXCVBNM0123456789 ";
                break;

            case ExpressionTypeEnum.Percent:
                revTextEdit.Enabled = true;
                revTextEdit_ValidatorCalloutExtender.Enabled = true;
                rfvTextEdit_ValidatorCalloutExtender.Enabled =false;
                rfvTextEdit.Enabled = false;
                revTextEdit.ValidationExpression = "^(100(?:.0{1,2})?%?|0*?.\\d{1,2}%?|\\d{1,2}(?:.\\d{1,2})?%?)$";
                tbTextEdit_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                tbTextEdit_FilteredTextBoxExtender.ValidChars = "0123456789";
                break;

            case ExpressionTypeEnum.BothCase:
                tbTextEdit_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                tbTextEdit_FilteredTextBoxExtender.ValidChars = "qwertyuiopasdfghjklñzxcvbnmQWERTYUIOPASDFGHJKLÑZXCVBNM ";
                break;

            case ExpressionTypeEnum.Parameter:
                tbTextEdit_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                tbTextEdit_FilteredTextBoxExtender.ValidChars = "qwertyuiopasdfghjklñzxcvbnmQWERTYUIOPASDFGHJKLÑZXCVBNM0123456789@_:/., ";
                break;
        } 

       
    }
    
    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 08/07/2011
    /// Descripcion: Establecer caracteres validos que se pueden introducir en el TextBox
    /// </summary>
    public void SetCustomValidator(string validChars)
    {
        rfvTextEdit.Enabled = false;
        tbTextEdit_FilteredTextBoxExtender.Enabled = true;
        tbTextEdit_TextBoxWatermarkExtender.Enabled = false;
        revTextEdit.Enabled = false;

        tbTextEdit_FilteredTextBoxExtender.ValidChars = validChars;
        tbTextEdit_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Custom;
    }   

    #endregion

    #region PROPIEDADES
    
    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 08/07/2011
    /// Descripcion: Ancho del Control Text Box, por defecto tiene 150 px
    /// </summary>
    public Unit Width
    {
        get { return tbTextEdit.Width; }
        set { tbTextEdit.Width = value; }
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 08/07/2011
    /// Descripcion: Deshabilidar o habilitar el control TextBox
    /// </summary>
    public bool Enable
    {
        get { return tbTextEdit.Enabled; }
        set { tbTextEdit.Enabled = value; }
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 08/07/2011
    /// Descripcion: Determina si el Valor relacionado al control es requerido
    /// </summary>
    public bool IsRequired
    {
        get { return rfvTextEdit.Enabled; }
        set { rfvTextEdit.Enabled = value; }
    }

    /// <summary>
    /// Autor: Manuel Gutiérrez Rojas
    /// Fecha: 13/07/2011
    /// Descripcion: Texto asociado al Control
    /// </summary>
    public string Text
    {
        get {return tbTextEdit.Text ;}
        set { tbTextEdit.Text = value; }
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 21/07/2011
    /// Descripcion: Cantidad máxima de caracteres del Control
    /// </summary>
    public int MaxLength
    {
        get { return tbTextEdit.MaxLength; }
        set { 
            tbTextEdit.MaxLength = value;
            if (tbTextEdit.TextMode == TextBoxMode.MultiLine) 
                tbTextEdit.Attributes["OnKeyPress"] = "return ValidateMaxLength(this," + value + ");";
        }
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 21/07/2011
    /// Descripcion: Indica si el TextBox es de tipo MultiLine o SingleLine
    /// </summary>
    public TextBoxMode TextMode
    {
        get { return tbTextEdit.TextMode; }
        set { tbTextEdit.TextMode = value; }
    }

    /// <summary>
    /// Autor: Olonyl Rocha Landeros
    /// Fecha: 21/07/2011
    /// Descripcion: Altura del TextBox, aplica para el caso que sea MultiLinea
    /// </summary>
    public Unit Height
    { 
        get { return tbTextEdit.Height; } 
        set{tbTextEdit.Height=value;} 
    }

    #endregion
}
