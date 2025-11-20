namespace UnityEngine.TextCore
{
	public class Glyph
	{
		public int index;
		public GlyphMetrics metrics;
		public GlyphRect glyphRect;
		public float scale;
		public int atlasIndex;
		
		// Here's the shims
		public int id => index;
        public float x => glyphRect.x;
        public float y => glyphRect.y;
        public float width => metrics.width;
        public float height => metrics.height;
        public float xOffset;
        public float yOffset;
        public float xAdvance => metrics.horizontalAdvance;
		
		public Glyph()
		{
			index = 0;
			metrics = default(GlyphMetrics);
			glyphRect = default(GlyphRect);
			scale = 1;
			atlasIndex = 0;
		}
		
		public Glyph(Glyph glyph)
		{
			index = glyph.index;
			metrics = glyph.metrics;
			glyphRect = glyph.glyphRect;
			scale = glyph.scale;
			atlasIndex = glyph.atlasIndex;
		}
		
		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect)
		{
			// this is a bad hack
			this.index = (int)index;
			this.metrics = metrics;
			this.glyphRect = glyphRect;
			scale = 1;
			atlasIndex = 0;
		}

		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			this.index = (int)index;
			this.metrics = metrics;
			this.glyphRect = glyphRect;
			this.scale = scale;
			this.atlasIndex = atlasIndex;
		}

		public bool Compare(Glyph other)
		{
			return index == other.index && metrics == other.metrics && glyphRect == other.glyphRect && scale == other.scale && atlasIndex == other.atlasIndex;
		}
	}
}