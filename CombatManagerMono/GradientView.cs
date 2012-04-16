using System;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace CombatManagerMono
{

	[Register("GradientView")]
	public class GradientView : UIView
	{
		UIColor _color1 = UIExtensions.RGBColor(0x222222);
		UIColor _color2 = UIExtensions.RGBColor(0xAAAAAA);
		float _cornerRadius = 8.0f;
		float _border = 1.0f;
		UIColor _borderColor = UIExtensions.RGBColor(0x0);
		GradientHelper _gradient;
		
		
		public GradientView (IntPtr p) : base(p)
		{

		}
		
		public GradientView ()
		{

		}
		
		
		
		public override void Draw (RectangleF rect)
		{
			rect.X += _border/2.0f;
			rect.Width -= _border;
			rect.Y += _border/2.0f;
			rect.Height -= _border;
			
			CGContext cr = UIGraphics.GetCurrentContext ();
			
			if (_gradient != null)
			{
				cr.DrawRoundRect(_gradient.Gradient, rect, _cornerRadius);
			}
			else
			{
				cr.DrawRoundRect(_color1, _color2, rect, _cornerRadius);
			}
			
			if (_border > 0)
			{
				GraphicUtils.RoundRectPath(cr, rect, _cornerRadius);
				cr.SetStrokeColor(_borderColor.CGColor.Components);
				cr.SetLineWidth(_border);
				cr.StrokePath();	
			}
			
		}
		
		public UIColor Color1
		{
			get
			{
				return _color1;
			}
			set
			{
				_color1 = value;
				SetNeedsDisplay();
			}
		}
		
		public UIColor Color2
		{
			get
			{
				return _color2;
			}
			set
			{
				_color2 = value;
				SetNeedsDisplay();
			}
		}
		
		public UIColor BorderColor
		{
			get
			{
				return _borderColor;
			}
			set
			{
				_borderColor = value;
				SetNeedsDisplay();
			}
		}
		
		public float CornerRadius
		{
			get
			{
				return _cornerRadius;
			}
			set
			{
				_cornerRadius = value;
				SetNeedsDisplay();
			}
		}
		
		public float Border
		{
			get
			{
				return _border;
			}
			set
			{
				_border = value;
				SetNeedsDisplay();
			}
		}
		
		
		public GradientHelper Gradient
		{
			get
			{
				return _gradient;
			}
			set
			{
				_gradient = value;
				SetNeedsDisplay();
			}
		}
	}
				
}

