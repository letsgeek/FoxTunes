﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace FoxTunes.Templates
{
    using FoxDb;
    using FoxTunes.Interfaces;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\personal\FoxTunes\FoxTunes.DB.SQLite\Templates\AddLibraryHierarchyNodeToPlaylist.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class AddLibraryHierarchyNodeToPlaylist : AddLibraryHierarchyNodeToPlaylistBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"
WITH ""VerticalMetaData""
AS
(
	SELECT ""LibraryItems"".""Id"", ""LibraryItems"".""FileName"", ""MetaDataItems"".""Name"", ""MetaDataItems"".""Value""
	FROM ""LibraryHierarchyItems""
		JOIN ""LibraryHierarchyItem_LibraryItem"" 
			ON ""LibraryHierarchyItems"".""Id"" = ""LibraryHierarchyItem_LibraryItem"".""LibraryHierarchyItem_Id""
		JOIN ""LibraryItems""
			ON ""LibraryItems"".""Id"" = ""LibraryHierarchyItem_LibraryItem"".""LibraryItem_Id""
		LEFT OUTER JOIN ""LibraryItem_MetaDataItem""
			ON ""LibraryItem_MetaDataItem"".""LibraryItem_Id"" = ""LibraryItems"".""Id"" 
		LEFT OUTER JOIN ""MetaDataItems"" 
			ON ""MetaDataItems"".""Id"" = ""LibraryItem_MetaDataItem"".""MetaDataItem_Id""
	WHERE ""LibraryHierarchyItems"".""Id"" = @libraryHierarchyItemId
		AND
		(
");
            
            #line 25 "C:\personal\FoxTunes\FoxTunes.DB.SQLite\Templates\AddLibraryHierarchyNodeToPlaylist.tt"

var first = true;
foreach (var name in this.Names)
{
	if (first)
	{
		first = false;
	}
	else
	{

            
            #line default
            #line hidden
            this.Write(" OR ");
            
            #line 35 "C:\personal\FoxTunes\FoxTunes.DB.SQLite\Templates\AddLibraryHierarchyNodeToPlaylist.tt"

	}

            
            #line default
            #line hidden
            this.Write("\"MetaDataItems\".\"Name\" = ");
            
            #line 37 "C:\personal\FoxTunes\FoxTunes.DB.SQLite\Templates\AddLibraryHierarchyNodeToPlaylist.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Database.QueryFactory.Dialect.String(name)));
            
            #line default
            #line hidden
            
            #line 37 "C:\personal\FoxTunes\FoxTunes.DB.SQLite\Templates\AddLibraryHierarchyNodeToPlaylist.tt"

}

            
            #line default
            #line hidden
            this.Write("\t\t)\r\n)\r\n,\r\n\"HorizontalMetaData\"\r\nAS\r\n(\r\n");
            
            #line 46 "C:\personal\FoxTunes\FoxTunes.DB.SQLite\Templates\AddLibraryHierarchyNodeToPlaylist.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(new PivotViewBuilder(
		this.Database,
		"VerticalMetaData", 
		new[] { "Id", "FileName" }, 
		new[] { "Name" }, 
		new[] { "Value" }, 
		this.Names
	).TransformText()));
            
            #line default
            #line hidden
            this.Write("\r\n)\r\n\r\nINSERT INTO \"PlaylistItems\" (\"Playlist_Id\", \"LibraryItem_Id\", \"Sequence\", " +
                    "\"DirectoryName\", \"FileName\", \"Status\", \"Flags\") \r\nSELECT @playlistId, \"LibraryIt" +
                    "ems\".\"Id\", @sequence + ROW_NUMBER() OVER\r\n(\r\n\tORDER BY\r\n");
            
            #line 62 "C:\personal\FoxTunes\FoxTunes.DB.SQLite\Templates\AddLibraryHierarchyNodeToPlaylist.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(new PlaylistSortBuilder(this.Database, this.Sort).TransformText()));
            
            #line default
            #line hidden
            this.Write(@"
) - 1, ""LibraryItems"".""DirectoryName"", ""LibraryItems"".""FileName"", @status, 0
FROM ""LibraryHierarchyItems""
	JOIN ""LibraryHierarchyItem_LibraryItem"" 
		ON ""LibraryHierarchyItems"".""Id"" = ""LibraryHierarchyItem_LibraryItem"".""LibraryHierarchyItem_Id""
	JOIN ""LibraryItems""
		ON ""LibraryItems"".""Id"" = ""LibraryHierarchyItem_LibraryItem"".""LibraryItem_Id""
	JOIN ""HorizontalMetaData""
		ON ""HorizontalMetaData"".""Id"" = ""LibraryItems"".""Id""
WHERE ""LibraryHierarchyItems"".""Id"" = @libraryHierarchyItemId
");
            
            #line 72 "C:\personal\FoxTunes\FoxTunes.DB.SQLite\Templates\AddLibraryHierarchyNodeToPlaylist.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(new LibraryHierarchyFilterBuilder(this.Database, this.Filter, LibraryHierarchyFilterSource.LibraryItem).TransformText()));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nSELECT COUNT(*)\r\nFROM \"PlaylistItems\"\r\nWHERE \"Playlist_Id\" = @playlistId\r\n\tA" +
                    "ND \"Status\" = @status");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class AddLibraryHierarchyNodeToPlaylistBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
