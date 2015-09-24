using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MissingAggregator>))]
	public interface IMissingAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		FieldName Field { get; set; }
	}

	public class MissingAggregator : BucketAggregator, IMissingAggregator
	{
		public FieldName Field { get; set; }
	}

	public class MissingAgg : BucketAgg, IMissingAggregator
	{
		public FieldName Field { get; set; }

		public MissingAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Missing = this;
	}

	public class MissingAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<MissingAggregatorDescriptor<T>,IMissingAggregator, T>
			, IMissingAggregator 
		where T : class
	{
		FieldName IMissingAggregator.Field { get; set; }

		public MissingAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public MissingAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

	}
}