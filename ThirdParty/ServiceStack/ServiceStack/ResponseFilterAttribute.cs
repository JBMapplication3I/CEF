namespace ServiceStack
{
    using Web;

    /// <summary>Base class to create response filter attributes only for specific HTTP methods (GET, POST...)</summary>
    /// <seealso cref="AttributeBase"/>
    /// <seealso cref="IHasResponseFilter"/>
    public abstract class ResponseFilterAttribute : AttributeBase, IHasResponseFilter
    {
        /// <inheritdoc/>
        public int Priority { get; set; }

        /// <summary>Gets or sets the apply to.</summary>
        /// <value>The apply to.</value>
        public ApplyTo ApplyTo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ResponseFilterAttribute"/> class.</summary>
        protected ResponseFilterAttribute()
        {
            ApplyTo = ApplyTo.All;
        }

        /// <summary>Creates a new <see cref="ResponseFilterAttribute"/></summary>
        /// <param name="applyTo">Defines when the filter should be executed.</param>
        protected ResponseFilterAttribute(ApplyTo applyTo)
        {
            ApplyTo = applyTo;
        }

        /// <inheritdoc/>
        public void ResponseFilter(IRequest req, IResponse res, object response)
        {
            var httpMethod = req.HttpMethodAsApplyTo();
            if (ApplyTo.Has(httpMethod))
            {
                Execute(req, res, response);
            }
        }

        /// <summary>This method is only executed if the HTTP method matches the <see cref="ApplyTo"/> property.</summary>
        /// <param name="req">        The http request wrapper.</param>
        /// <param name="res">        The http response wrapper.</param>
        /// <param name="responseDto">The response DTO.</param>
        public abstract void Execute(IRequest req, IResponse res, object responseDto);

        /// <summary>Create a ShallowCopy of this instance.</summary>
        /// <returns>An IHasResponseFilter.</returns>
        public virtual IHasResponseFilter Copy()
        {
            return (IHasResponseFilter)MemberwiseClone();
        }
    }
}
