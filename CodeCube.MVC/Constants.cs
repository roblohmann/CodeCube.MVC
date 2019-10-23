namespace CodeCube.Mvc
{
    public static class Constants
    {
        /// <summary>
        /// The maximum allowed length of an url slug
        /// </summary>
        public const int MaxLengthUrlSlug = 255;

        public const string HtmlRegexPattern = "<.*?>";

        /// <summary>
        /// An string of stopwords. All these words should be ignored before generating meta keywords.
        /// </summary>
        public const string DutchStopWords = "aan,af,al,als,bij,dan,dat,die,dit,een,en,er,had,heb,hem,het,hij,hoe,hun,ik,in,is,je,kan,me,men,met,mij,nog,nu,of,ons,ook,te,tot,uit,van,was,wat,we,wel,wij,zal,ze,zei,zij,zo,zou";
    }
}
