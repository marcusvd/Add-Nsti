export interface TreeNode {
  name: string;
  route?: string;
  icon?: string;
  toolTip?: string;
  opened?: boolean;
  divider?: boolean;
  children?: TreeNode[];
}
