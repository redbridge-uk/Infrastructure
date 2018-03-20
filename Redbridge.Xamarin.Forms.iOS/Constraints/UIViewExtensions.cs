using System;
using UIKit;

namespace Redbridge.Xamarin.Forms.iOS.Constraints
{
    public static class UIViewExtensions
    {

        public static UIView AddWidthConstraint(this UIView uiView, float width)
        {
            if (uiView == null) throw new ArgumentNullException(nameof(uiView));
            uiView.TranslatesAutoresizingMaskIntoConstraints = false;
            uiView.AddConstraint(NSLayoutConstraint.Create(uiView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, width));
            return uiView;
        }

        public static UIView AddHeightConstraint(this UIView uiView, float height)
        {
            if (uiView == null) throw new ArgumentNullException(nameof(uiView));
            uiView.TranslatesAutoresizingMaskIntoConstraints = false;
            uiView.AddConstraint(NSLayoutConstraint.Create(uiView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, height));
            return uiView;
        }

        public static void AddWidthAndHeightConstraints (this UIView uiView, float width, float height)
        {
            if (uiView == null) throw new ArgumentNullException(nameof(uiView));
            uiView.TranslatesAutoresizingMaskIntoConstraints = false;
            uiView.AddConstraint(NSLayoutConstraint.Create(uiView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, height));
            uiView.AddConstraint(NSLayoutConstraint.Create(uiView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, width));
        }

        public static void AddVerticalAlignmentConstraint(this UIView parentView, UIView subView)
        { 
            parentView.AddConstraint(NSLayoutConstraint.Create(parentView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, subView, NSLayoutAttribute.CenterY, 1, 0));
        }

        public static void AddLeftConstraint(this UIView parentView, UIView subView, float leftMargin)
        {
            parentView.AddConstraint(NSLayoutConstraint.Create(subView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, parentView, NSLayoutAttribute.Left, 1, leftMargin));
        }

        public static void AddTopConstraint(this UIView parentView, UIView subView, float top)
        {
            parentView.AddConstraint(NSLayoutConstraint.Create(subView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, parentView, NSLayoutAttribute.Top, 1, top));
        }

        public static void AddLeftMarginConstraintBetween (this UIView parentView, UIView leftView, UIView rightView, float leftMargin)
        {
            parentView.AddConstraint(NSLayoutConstraint.Create(rightView, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, leftView, NSLayoutAttribute.Trailing, 1, leftMargin));
        }

        public static void AddTopMarginConstraintBetween(this UIView parentView, UIView upperView, UIView lowerView, float topMargin)
        {
            parentView.AddConstraint(NSLayoutConstraint.Create(upperView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, lowerView, NSLayoutAttribute.Top, 1, topMargin));
        }
    }
}
