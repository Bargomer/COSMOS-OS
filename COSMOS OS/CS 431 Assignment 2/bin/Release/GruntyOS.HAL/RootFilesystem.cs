using GruntyOS.String;
using System;
using System.Collections.Generic;
namespace GruntyOS.HAL
{
	public class RootFilesystem : StorageDevice
	{
		public List<MountPoint> Mountpoints = new List<MountPoint>();
		public char Seperator = '/';
		public void Mount(string dir, StorageDevice sd)
		{
			MountPoint mountPoint = new MountPoint();
			dir = Util.cleanName(dir);
			mountPoint.Path = dir;
			mountPoint.device = sd;
			this.Mountpoints.Add(mountPoint);
		}
		public bool isMountPoint(string dir)
		{
			bool result;
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (Util.cleanName(this.Mountpoints[i].Path) == Util.cleanName(dir))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public void Unmount(string device)
		{
			List<MountPoint> list = new List<MountPoint>();
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (Util.cleanName(this.Mountpoints[i].Path) != Util.cleanName(device))
				{
					list.Add(this.Mountpoints[i]);
				}
			}
			this.Mountpoints = list;
		}
		public override void Chmod(string dir, string perms)
		{
			dir = Util.cleanName(dir);
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path && this.Mountpoints[i].Path != "")
					{
						((GLNFS)this.Mountpoints[i].device).Chmod(dir.Substring(this.Mountpoints[i].Path.Length), perms);
						return;
					}
				}
			}
			((GLNFS)this.Mountpoints[0].device).Chmod(dir, perms);
		}
		public override void Chown(string dir, string perms)
		{
			dir = Util.cleanName(dir);
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path && this.Mountpoints[i].Path != "")
					{
						((GLNFS)this.Mountpoints[i].device).Chown(dir.Substring(this.Mountpoints[i].Path.Length), perms);
						return;
					}
				}
			}
			((GLNFS)this.Mountpoints[0].device).Chown(dir, perms);
		}
		public override string[] ListJustFiles(string dir)
		{
			dir = Util.cleanName(dir);
			string[] result;
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path)
					{
						result = this.Mountpoints[i].device.ListJustFiles(dir.Substring(this.Mountpoints[i].Path.Length));
						return result;
					}
				}
			}
			result = this.Mountpoints[0].device.ListJustFiles(dir);
			return result;
		}
		public override fsEntry[] getLongList(string dir)
		{
			dir = Util.cleanName(dir);
			fsEntry[] longList;
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path)
					{
						longList = this.Mountpoints[i].device.getLongList(dir.Substring(this.Mountpoints[i].Path.Length));
						return longList;
					}
				}
			}
			longList = this.Mountpoints[0].device.getLongList(dir);
			return longList;
		}
		public override void Move(string dir, string dir2)
		{
			dir = Util.cleanName(dir);
			dir2 = Util.cleanName(dir2);
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path)
					{
						this.Mountpoints[i].device.Move(dir.Substring(this.Mountpoints[i].Path.Length), dir2.Substring(this.Mountpoints[i].Path.Length));
						return;
					}
				}
			}
			this.Mountpoints[0].device.Move(dir, dir2);
		}
		public override void makeDir(string dir, string owner)
		{
			dir = Util.cleanName(dir);
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path)
					{
						this.Mountpoints[i].device.makeDir(dir.Substring(this.Mountpoints[i].Path.Length), owner);
						return;
					}
				}
			}
			this.Mountpoints[0].device.makeDir(dir, owner);
		}
		public override byte[] readFile(string dir)
		{
			dir = Util.cleanName(dir);
			byte[] result;
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path)
					{
						result = this.Mountpoints[i].device.readFile(dir.Substring(this.Mountpoints[i].Path.Length));
						return result;
					}
				}
			}
			result = this.Mountpoints[0].device.readFile(dir);
			return result;
		}
		public override void saveFile(byte[] data, string dir, string owner)
		{
			dir = Util.cleanName(dir);
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path)
					{
						this.Mountpoints[i].device.saveFile(data, dir.Substring(this.Mountpoints[i].Path.Length), owner);
						return;
					}
				}
			}
			this.Mountpoints[0].device.saveFile(data, dir, owner);
		}
		public override void Delete(string dir)
		{
			dir = Util.cleanName(dir);
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path)
					{
						this.Mountpoints[i].device.Delete(dir.Substring(this.Mountpoints[i].Path.Length));
						return;
					}
				}
			}
			this.Mountpoints[0].device.Delete(dir);
		}
		public override string[] ListDirectories(string dir)
		{
			dir = Util.cleanName(dir);
			string[] result;
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path)
					{
						result = this.Mountpoints[i].device.ListDirectories(dir.Substring(this.Mountpoints[i].Path.Length));
						return result;
					}
				}
				else
				{
					if (this.Mountpoints[i].Path == "")
					{
					}
				}
			}
			result = this.Mountpoints[0].device.ListDirectories(dir);
			return result;
		}
		public override string[] ListFiles(string dir)
		{
			dir = Util.cleanName(dir);
			string[] result;
			for (int i = 0; i < this.Mountpoints.Count; i++)
			{
				if (this.Mountpoints[i].Path.Length <= dir.Length && this.Mountpoints[i].Path != "")
				{
					if (dir.Substring(0, this.Mountpoints[i].Path.Length) == this.Mountpoints[i].Path)
					{
						result = this.Mountpoints[i].device.ListFiles(dir.Substring(this.Mountpoints[i].Path.Length));
						return result;
					}
				}
				else
				{
					if (this.Mountpoints[i].Path == "")
					{
					}
				}
			}
			result = this.Mountpoints[0].device.ListFiles(dir);
			return result;
		}
	}
}
